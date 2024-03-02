package com.example.team10;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.FirebaseFirestore;

import java.util.ArrayList;
import java.util.Map;

public class MatchingActivity extends AppCompatActivity {
    ListView FriendListView;
    ArrayList<Friend> friendlist;
    FriendAdapter friendAdapter;

    private FirebaseAuth firebaseAuth;

    @Nullable
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_matching);
        FriendListView = findViewById(R.id.FriendListView);
        friendlist = new ArrayList<Friend>();
        friendlist.add(new Friend("초 코"));
        friendlist.add(new Friend("바닐라"));
        friendAdapter = new FriendAdapter(friendlist, getApplicationContext());
        FriendListView.setAdapter(friendAdapter);


        final FirebaseFirestore db = FirebaseFirestore.getInstance();

        FriendListView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                String text = friendlist.get(i).user_nickname;

                View inflatedView = getLayoutInflater().inflate(R.layout.friend_item, null);
                ImageView iUserIcon = (ImageView) inflatedView.findViewById(R.id.user_icon);
                TextView iUserNickname = (TextView) inflatedView.findViewById(R.id.user_nickname);
                TextView iUserTier = (TextView) inflatedView.findViewById(R.id.user_tier);
                TextView iUserLevel = (TextView) inflatedView.findViewById(R.id.user_level);
                TextView iUserManner = (TextView) inflatedView.findViewById(R.id.user_manner);
                TextView iUserMbti = (TextView) inflatedView.findViewById(R.id.user_mbti);
                iUserIcon.setImageBitmap(friendlist.get(i).user_icon);
                iUserNickname.setText(friendlist.get(i).user_nickname);
                iUserTier.setText("Tier: "+friendlist.get(i).user_tier);
                iUserLevel.setText("Level: "+friendlist.get(i).user_level);
                iUserManner.setText("Manner: "+friendlist.get(i).user_manner);
                iUserMbti.setText("MBTI: "+friendlist.get(i).user_mbti);

                final AlertDialog.Builder alt_blt = new AlertDialog.Builder(MatchingActivity.this, R.style.plus_id_register_dialog_style);
                alt_blt.setCancelable(false)
                        .setView(inflatedView)
                        .setPositiveButton("대화 하기", new DialogInterface.OnClickListener() {
                            @Override
                            public void onClick(DialogInterface dialogInterface, int t) {
                                String text = friendlist.get(i).user_nickname;
                                Intent intent = new Intent(MatchingActivity.this,ChatRoomActivity.class);
                                intent.putExtra("username",text);
                                startActivity(intent);
                            }
                        })
                        .setNegativeButton("친구 추가", new DialogInterface.OnClickListener() {
                            @Override
                            public void onClick(DialogInterface dialogInterface, int t) {
                                firebaseAuth = FirebaseAuth.getInstance();
                                FirebaseUser currentUser = firebaseAuth.getCurrentUser();
                                String uid = currentUser.getUid();
                                db.collection("users").document(uid).get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
                                    @Override
                                    public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                                        DocumentSnapshot document = task.getResult();
                                        if (document.exists()){
                                            Map<String, Object> userInfo = document.getData();
                                            String friends = userInfo.get("friend").toString();

                                            String text = friendlist.get(i).user_nickname;
                                            friends = friends + text + ',';
                                            db.collection("users").document(uid).update("friend", friends);
                                        }
                                    }
                                });
                            }
                        });
                AlertDialog dialog = alt_blt.create();
                dialog.show();


                //Intent intent = new Intent(MatchingActivity.this ,ChatRoomActivity.class);
                //intent.putExtra("username",text);
                //startActivity(intent);
            }
        });
    }

    @Override
    public void onStart() {
        super.onStart();


    }
}