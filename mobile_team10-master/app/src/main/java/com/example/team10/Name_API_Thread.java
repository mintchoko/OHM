package com.example.team10;


import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;

import androidx.activity.result.contract.ActivityResultContracts;
import androidx.annotation.Nullable;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;

class Name_API_Thread extends Thread{
    //api key 매번 갱신
    String TOKEN = "RGAPI-d2e56a66-105f-4e59-9c80-c27229796759";
    private String Summoners_name;
    private String Summoners_id;
    private int Summoners_level;
    private String Summoners_tier;
    private String Summoners_rank;
    private int Summoners_win, Summoners_lose;
    private boolean is_success;
    private int Summoners_icon;
    private Bitmap Summoners_bitmap;

    public Name_API_Thread(){}
    public Name_API_Thread(String Summoners_name){
        this.Summoners_name = Summoners_name;
    }

    @Nullable public JSONObject get(String strUrl){
        try{
            URL url = new URL(strUrl);
            HttpURLConnection con = (HttpURLConnection) url.openConnection();
            con.setConnectTimeout(1000);
            con.setReadTimeout(1000);
            con.setRequestMethod("GET");
            con.setDoOutput(false);

            Log.d("URL :", con.getURL().toString());

            StringBuilder sb = new StringBuilder();

            if(con.getResponseCode() == HttpURLConnection.HTTP_OK){
                //url에서 버퍼 형태로 데이터를 받음
                BufferedReader br = new BufferedReader(new InputStreamReader(con.getInputStream(), "utf-8"));
                String line = br.readLine();
                while((line != null)){
                    sb.append(line).append("\n");
                    line = br.readLine();
                }
                br.close();
                Log.d("sb string: ", sb.toString());
                String result = (sb.indexOf("[") == -1) ? sb.toString() : sb.substring(1, sb.length() - 2);
                //JSON 형태로 변환 후 리턴
                JSONObject jsonObj = new JSONObject(result);

                return jsonObj;
            }else{
                Log.d("wrong response: ", con.getResponseMessage());
            }
        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return null;
    }
    //run 메서드는 summoner의 정보를 얻기 위해 필요한 encrypte_ID를 받기 위해 닉네임으로 api를 가져오는 메서드. 이 메서드에서 getInfo 메서드를 실행한다.
    @Override
    public void run() {
        String SummonerName = this.Summoners_name.replaceAll(" ", "%20");
        String requestURL = "https://kr.api.riotgames.com/lol/summoner/v4/summoners/by-name/" + SummonerName + "?api_key=" + TOKEN;
        //API lol_api = new API();
        JSONObject jsonObj = get(requestURL);
        try {

            if(jsonObj == null){ is_success = false; return; }
            else{ is_success = true;}
            Summoners_id = (String) jsonObj.get("id");
            Summoners_level = (int) jsonObj.get("summonerLevel");
            Summoners_icon = (int) jsonObj.get("profileIconId");
            Summoners_bitmap = getImageFromUrl("https://ddragon.leagueoflegends.com/cdn/10.6.1/img/profileicon/"+getSummoners_info("icon")+".png");

            // 현재 레벨만 로그에 띄움
            Log.d("Succsess : ", "get ID");
            getInfo();

        } catch (JSONException e) {
            e.printStackTrace();
        }

    }
    //받아온 ID를 가지고 티어, 숙련도 등을 가져오는 메서드
    public void getInfo(){
        Log.d("Summoners_id", Summoners_id);
        String requestURL = "https://kr.api.riotgames.com/lol/league/v4/entries/by-summoner/"+Summoners_id+"?api_key="+TOKEN;
        JSONObject jsonObj = get(requestURL);

        try {

            Summoners_tier = (String) jsonObj.get("tier");
            Summoners_rank = (String) jsonObj.get("rank");
            Summoners_win = (int) jsonObj.get("win");
            Summoners_lose = (int) jsonObj.get("lose");
            Log.d("Success: ", "get info");
        } catch (JSONException e) {
            e.printStackTrace();
        } catch (java.lang.NullPointerException e){
            is_success = false;
        }
    }
    public Bitmap getImageFromUrl(String urlStr){
        Bitmap imgBitmap = null;
        try{
            URL url = new URL(urlStr);
            URLConnection con = url.openConnection();
            con.connect();
            BufferedInputStream bf = new BufferedInputStream(con.getInputStream());
            imgBitmap = BitmapFactory.decodeStream(bf);
            bf.close();
        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return imgBitmap;
    }
    public Bitmap getSummoners_bitmap(){
        return Summoners_bitmap;
    }
    public String getSummoners_info(String needs){
        switch(needs){
            case "is_success":
                return String.valueOf(is_success);
            case "id":
                return Summoners_id;
            case "name":
                return Summoners_name;
            case "tier":
                if(Summoners_tier == null){Summoners_tier = "X";}
                return Summoners_tier;
            case "rank":
                if(Summoners_rank == null){Summoners_rank = "";}
                return Summoners_rank;
            case "level":
                return String.valueOf(Summoners_level);
            case "icon":
                return String.valueOf(Summoners_icon);
            default:
                return "Dont know";
        }
    }

}
