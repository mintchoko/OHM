package com.example.team10;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.util.Base64;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.firestore.CollectionReference;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.FirebaseFirestore;

import java.io.ByteArrayOutputStream;
import java.util.HashMap;
import java.util.Map;


public class MainActivity extends Fragment{
    private FirebaseAuth firebaseAuth;
    final FirebaseFirestore db = FirebaseFirestore.getInstance();
    int is_registered = 0;
    Button plus_id_register;
    Button matchingBtn;

    Button friendBtn;
    Button userInfo;

    Name_API_Thread apiThread;

    View root;

//    String register_file = "register_file";
//    SharedPreferences sharedPreferences;

    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        root = inflater.inflate(R.layout.activity_main, container, false);
        firebaseAuth = FirebaseAuth.getInstance();
        FirebaseUser currentUser = firebaseAuth.getCurrentUser();
        String uid = currentUser.getUid();

        matchingBtn = (Button) root.findViewById(R.id.matchingBtn);
        plus_id_register = (Button) root.findViewById(R.id.plus_id_register);

//        sharedPreferences = getSharedPreferences(register_file, 0);
//        if(!sharedPreferences.getString("nickname", "").equals("")){
//            register_by_nickname(sharedPreferences.getString("nickname", ""));
//            setTextView_register_by_nickname();
//        }

        db.collection("lol").document(uid).get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
            public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                DocumentSnapshot document = task.getResult();
                if (document.exists()) {
                    register_by_nickname(document.getData().get("name").toString());
                    setTextView_register_by_nickname();
                    is_registered = 1;
                }
            }

        });

        matchingBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onClickMatchingBtn(view);
            }
        });
        plus_id_register.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onClickRegisterBtn(view);
            }
        });
        return root;
    }


    /* String <=> Bitmap */
    public static Bitmap StringToBitmap(String encodedString) {
        try {
            byte[] encodeByte = Base64.decode(encodedString, Base64.DEFAULT);
            Bitmap bitmap = BitmapFactory.decodeByteArray(encodeByte, 0, encodeByte.length);
            return bitmap;
        } catch (Exception e) {
            e.getMessage();
            return null;
        }
    }
    public static String BitmapToString(Bitmap bitmap) {
        if (bitmap == null) {
            return "";
        }
        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        bitmap.compress(Bitmap.CompressFormat.PNG, 70, baos);
        byte[] bytes = baos.toByteArray();
        String temp = Base64.encodeToString(bytes, Base64.DEFAULT);
        return temp;
    }
    public static byte[] BitmapToByteArray(Bitmap bitmap) {
        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        bitmap.compress(Bitmap.CompressFormat.JPEG, 70, baos);
        return baos.toByteArray();

    }


    public boolean register_by_nickname(String value){
        apiThread = new Name_API_Thread(value);
        try {
            apiThread.start();
            apiThread.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        if(apiThread.getSummoners_info("is_success") == "false"){ return false; }

        String nickname = value;
        String level = apiThread.getSummoners_info("level");
        String tier = apiThread.getSummoners_info("tier");
        String rank = apiThread.getSummoners_info("rank");
        Bitmap icon = apiThread.getSummoners_bitmap();
        String sicon = BitmapToString(icon);

        HashMap<Object,String> lolUserInfo = new HashMap<>();
        lolUserInfo.put("name", nickname);
        lolUserInfo.put("level", level);
        lolUserInfo.put("tier", tier);
        lolUserInfo.put("icon", sicon);
        lolUserInfo.put("rank", rank);


        firebaseAuth = FirebaseAuth.getInstance();
        FirebaseUser currentUser = firebaseAuth.getCurrentUser();
        String uid = currentUser.getUid();

        db.collection("lol").document(uid)
                .set(lolUserInfo).addOnFailureListener(new OnFailureListener() {
            @Override
            public void onFailure(@NonNull Exception e) {
                Toast.makeText(getActivity().getApplicationContext(), e.toString(), Toast.LENGTH_SHORT).show();
            }
        });

//        SharedPreferences.Editor editor = sharedPreferences.edit();
//        editor.putString("nickname", value);
//        editor.commit();

        return true;
    }
    public void setTextView_register_by_nickname(){
        firebaseAuth = FirebaseAuth.getInstance();
        FirebaseUser currentUser = firebaseAuth.getCurrentUser();
        String uid = currentUser.getUid();

        db.collection("lol").document(uid).get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
            @SuppressLint("SetTextI18n")
            @Override
            public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                DocumentSnapshot document = task.getResult();
                if (document.exists()){
                    Map<String, Object> lolUserInfo = document.getData();
                    String name = lolUserInfo.get("name").toString();
                    String level = lolUserInfo.get("level").toString();
                    String tier = lolUserInfo.get("tier").toString();
                    String rank = lolUserInfo.get("rank").toString();
                    Bitmap icon = StringToBitmap(lolUserInfo.get("icon").toString());

                    LinearLayout user_layout = (LinearLayout) root.findViewById(R.id.id_register_item);
                    plus_id_register.setVisibility(View.GONE);
                    user_layout.setVisibility(View.VISIBLE);

                    TextView user_nickname = (TextView) root.findViewById(R.id.user_nickname);
                    TextView user_level = (TextView) root.findViewById(R.id.user_level);
                    TextView user_tier = (TextView) root.findViewById(R.id.user_tier);
                    TextView user_mbti = (TextView) root.findViewById(R.id.user_mbti);
                    TextView user_manner = (TextView) root.findViewById(R.id.user_manner);

                    user_nickname.setText(name);
                    user_level.setText("Level: " + level);
                    user_tier.setText("Tier: " + tier + " " + rank);
                    user_mbti.setText("MBTI: "+ "mbti");
                    user_manner.setText("Manner: "+ "manner");

                    ImageView user_icon = (ImageView) root.findViewById(R.id.user_icon);
                    user_icon.setImageBitmap(icon);


                    LinearLayout.LayoutParams mLayoutParams = (LinearLayout.LayoutParams) user_layout.getLayoutParams();
                    mLayoutParams.bottomMargin = 100;
                    user_layout.setLayoutParams(mLayoutParams);

                }
            }
        });
    }



    public void onClickMatchingBtn(View view) {
        if(is_registered == 0){
            Toast.makeText(root.getContext().getApplicationContext(), "유저등록을 먼저 해주세요", Toast.LENGTH_SHORT).show();
        }
        else {
            Intent intent = new Intent(getActivity(), MatchingActivity.class);
            startActivity(intent);
        }
    }

    public void onClickRegisterBtn(View view){
        //Dialog로 닉네임를 받아서 api를 통해 정보를 가져와 ImageView에 입력.
        EditText et = new EditText(root.getContext().getApplicationContext());
        et.setSingleLine(true);

        //닉네임을 받는 dialog
        final AlertDialog.Builder alt_blt = new AlertDialog.Builder(root.getContext(), R.style.plus_id_register_dialog_style);
        alt_blt.setTitle("아이디 생성")
                .setMessage("닉네임을 적어주세요")
                .setCancelable(false)
                .setView(et)
                .setPositiveButton("확인", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        is_registered = 1;
                        String value = et.getText().toString();
                        /* 롤 API 사용자 정보 호출 */
                        if (!register_by_nickname(value)){
                            Toast.makeText(root.getContext().getApplicationContext(), "존재하지않는 사용자입니다.", Toast.LENGTH_SHORT).show();
                            return;
                        }
                        setTextView_register_by_nickname();
                    }
                })
                .setNegativeButton("취소", new DialogInterface.OnClickListener(){
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        is_registered = 0;
                    }
                });

        AlertDialog dialog = alt_blt.create();
        dialog.show();

    }
}
