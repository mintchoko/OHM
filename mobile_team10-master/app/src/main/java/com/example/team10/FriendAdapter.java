package com.example.team10;

import android.content.Context;
import android.graphics.Bitmap;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import java.util.ArrayList;

public class FriendAdapter extends BaseAdapter {
    Context context;
    ArrayList<Friend> FriendList;

    public FriendAdapter(ArrayList<Friend> FriendList, Context context){
        this.FriendList = FriendList;
        this.context = context;
    }

    @Override
    public int getCount() {
        return FriendList.size();
    }

    @Override
    public Object getItem(int i) {
        return FriendList.get(i);
    }

    @Override
    public long getItemId(int i) {
        return i;
    }

    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {
        LayoutInflater inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        view = inflater.inflate(R.layout.friend_item, viewGroup, false);

        String value = FriendList.get(i).user_nickname;
        Name_API_Thread apiThread = new Name_API_Thread(value);
        try {
            apiThread.start();
            apiThread.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        Log.d("Adapter", "Successful");
        TextView user_nickname = view.findViewById(R.id.user_nickname);
        TextView user_level = view.findViewById(R.id.user_level);
        TextView user_tier = view. findViewById(R.id.user_tier);
        TextView user_mbti = view.findViewById(R.id.user_mbti);
        TextView user_manner = view.findViewById(R.id.user_manner);


        user_nickname.setText(value);
        user_level.setText("Level: " + apiThread.getSummoners_info("level"));
        user_tier.setText("Tier: " + apiThread.getSummoners_info("tier") + " " + apiThread.getSummoners_info("rank"));
        user_mbti.setText("MBTI: " + apiThread.getSummoners_info("mbti"));
        user_manner.setText("Manner: "+apiThread.getSummoners_info("manner"));

        ImageView user_icon = view.findViewById(R.id.user_icon);
        Bitmap uBitmap = apiThread.getSummoners_bitmap();
        user_icon.setImageBitmap(uBitmap);

        FriendList.get(i).user_icon = uBitmap;
        FriendList.get(i).user_tier = apiThread.getSummoners_info("tier") + " " + apiThread.getSummoners_info("rank");
        FriendList.get(i).user_level = apiThread.getSummoners_info("level");
        FriendList.get(i).user_mbti = apiThread.getSummoners_info("mbti");
        FriendList.get(i).user_manner = apiThread.getSummoners_info("manner");


        return view;
    }
}
