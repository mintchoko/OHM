package com.example.team10;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.FirebaseFirestore;

import org.w3c.dom.Text;

import java.util.Map;

public class UserActivity extends Fragment {
    private FirebaseAuth firebaseAuth;
    final FirebaseFirestore db = FirebaseFirestore.getInstance();

    Button BtnLogout;
    Button BtnWithdraw;

    TextView TvMbtiTest;
    TextView TvEmail;
    TextView TvMbti;
    TextView TvLolUsername;
    TextView TvLolUserConnect;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.activity_user, container, false);


        return view;
    }

    @Override
    public void onStart() {
        super.onStart();

        TvMbtiTest = (TextView) getView().findViewById(R.id.mbtiTest);

        TvEmail = (TextView) getView().findViewById(R.id.email_userInfo);
        TvMbti = (TextView) getView().findViewById(R.id.mbti_userInfo);
        TvLolUsername = (TextView) getView().findViewById(R.id.lol_username);
        TvLolUserConnect = (TextView) getView().findViewById(R.id.lol_userConnect);

        // logout button click event
        BtnLogout = (Button) getView().findViewById(R.id.btnLogout) ;
        BtnLogout.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                onClickLogoutBtn(v);
            }
        });

        // mbti test click event
        TvMbtiTest.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getActivity(),TestActivity.class);
                startActivity(intent);
            }
        });

        TvLolUserConnect.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (TvLolUserConnect.getText().equals("연동해제")){
                    onClickUnlink(v, "lol"); // test
                }
            }
        });


        // userInfo
        firebaseAuth = FirebaseAuth.getInstance();
        FirebaseUser currentUser = firebaseAuth.getCurrentUser();

        String uid = currentUser.getUid();


        if (currentUser != null ){
            // email
            String email = currentUser.getEmail();
            TvEmail.setText(email);

            // mbti
            db.collection("users").document(uid).get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
                @Override
                public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                    DocumentSnapshot document = task.getResult();
                    if (document.exists()) {
                        Map<String, Object> userInfo = document.getData();
                        String mbti = userInfo.get("mbti").toString();
                        if (!mbti.equals("")){
                            TvMbti.setText(mbti);
                        }
                    }
                }
            });

            // game(lol)
            db.collection("lol").document(uid).get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
                @Override
                public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                    DocumentSnapshot document = task.getResult();
                    if (document.exists()){
                        Map<String, Object> userInfo = document.getData();
                        String username = userInfo.get("name").toString();
                        TvLolUsername.setText(username);
                        TvLolUserConnect.setText("연동해제");
                    }
                }
            });
        } else{
            // not current user
            Toast.makeText(getActivity(),"not current User", Toast.LENGTH_SHORT).show();
        }


    }

    public void onClickUnlink(View view, String game){
//        Toast.makeText(getActivity(),"click unlink", Toast.LENGTH_SHORT).show();
        FirebaseUser currentUser = firebaseAuth.getCurrentUser();
        String uid = currentUser.getUid();

        db.collection(game).document(uid).delete().addOnCompleteListener(new OnCompleteListener<Void>() {
            @Override
            public void onComplete(@NonNull Task<Void> task) {
                TvLolUsername.setText("");
                TvLolUserConnect.setText("미연동");
            }
        });
    }


    public void onClickLogoutBtn(View view){
        AlertDialog.Builder builder = new AlertDialog.Builder(getView().getContext());
        builder.setTitle("로그아웃").setMessage("로그아웃 하시겠습니까?");
        builder.setPositiveButton("확인", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                try {
                    firebaseAuth.signOut();
                } catch (Exception e){
                    Toast.makeText(getActivity(), "로그아웃에 오류가 발생했습니다.", Toast.LENGTH_LONG).show();
                    return;
                }
                Intent intent = new Intent(getView().getContext(), LoginActivity.class);
                startActivity(intent);
                getActivity().finish();
            }
        });
        builder.setNegativeButton("취소", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                return;
            }
        });
        AlertDialog alertDialog = builder.create();
        alertDialog.show();
    }

}