package com.example.team10;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ListView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.FirebaseFirestore;

import java.util.ArrayList;
import java.util.Map;

public class FriendActivity extends Fragment {
    private FirebaseAuth firebaseAuth;
    final FirebaseFirestore db = FirebaseFirestore.getInstance();

    ListView FriendListView;
    ArrayList<Friend> friendlist;
    FriendAdapter friendAdapter;
    Button chatBtn;
    public static Context mContext;
    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {

        return inflater.inflate(R.layout.activity_friend, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        chatBtn = (Button) getActivity().findViewById(R.id.chatBtn);
        chatBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                onClickChattingBtn(v);
            }
        });

        FriendListView = getView().findViewById(R.id.FriendListView);
        firebaseAuth = FirebaseAuth.getInstance();
        FirebaseUser currentUser = firebaseAuth.getCurrentUser();
        String uid = currentUser.getUid();

        db.collection("users").document(uid).get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
            @Override
            public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                DocumentSnapshot document = task.getResult();
                if (document.exists()){
                    Map<String, Object> userInfo = document.getData();
                    String[] friends = userInfo.get("friend").toString().split(",");
                    friendlist = new ArrayList<Friend>();
                    for (int i=0; i<friends.length; i++){
                        friendlist.add(new Friend(friends[i]));
                    }
                    friendAdapter = new FriendAdapter(friendlist, getActivity().getApplicationContext());
                    FriendListView.setAdapter(friendAdapter);
                }
            }
        });

    }
    public void addFriendView(String name){
        friendlist.add(new Friend(name));
    }
    public void onClickChattingBtn(View view){
        Intent intent = new Intent(getView().getContext(), ChatActivity.class);
        startActivity(intent);
    }
}
