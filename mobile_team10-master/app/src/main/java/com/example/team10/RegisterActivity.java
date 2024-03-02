package com.example.team10;

import androidx.annotation.ColorInt;
import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.Task;
import com.google.api.Distribution;
import com.google.firebase.auth.AuthResult;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.firestore.FirebaseFirestore;

import org.w3c.dom.Text;

import java.util.HashMap;

public class RegisterActivity extends AppCompatActivity {

    private FirebaseAuth firebaseAuth;
    final FirebaseFirestore db = FirebaseFirestore.getInstance();

    Button BtnRegister;

    EditText EtEmail;
    EditText EtPassword;
    EditText EtPasswordCheck;
    EditText EtUsername;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        BtnRegister = (Button) findViewById(R.id.btnRegister_register);

        EtEmail = (EditText) findViewById(R.id.etEmail_register);
        EtPassword = (EditText) findViewById(R.id.etPassword_register);
        EtPasswordCheck = (EditText) findViewById(R.id.etPasswordCheck_register);

        /* 비밀번호 재입력 시 값이 동일한지 비교 */
        EtPasswordCheck.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) { /* 텍스트 입력 전 call back */ }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

                String password = EtPassword.getText().toString();
                String checkPassword = EtPasswordCheck.getText().toString();

                TextView TvPasswordCheck = (TextView) findViewById(R.id.tvPasswordCheck_register);

                if (!password.equals(checkPassword)){
                    TvPasswordCheck.setText("비밀번호가 일치하지 않습니다.");
                    TvPasswordCheck.setTextColor(Color.parseColor("#E53C7F"));
                } else{
                    TvPasswordCheck.setText("");
                }
            }

            @Override
            public void afterTextChanged(Editable s) { /* 텍스트 입력 후 call back */ }
        });

        EtPassword.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) { }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                String password = EtPassword.getText().toString();

                TextView TvPassword = (TextView) findViewById(R.id.tvPassword_register);

                if (password.length() < 6){
                    TvPassword.setText("6자리 이상 입력해주세요.");
                    TvPassword.setTextColor(Color.parseColor("#E53C7F"));
                } else{
                    TvPassword.setText("");
                }
            }

            @Override
            public void afterTextChanged(Editable s) { }
        });
    }


    public void onClickBtnRegister(View view){
        String email = EtEmail.getText().toString();
        String password = EtPassword.getText().toString();
        String checkPassword = EtPasswordCheck.getText().toString();

        if (email.equals("") || password.equals("") || checkPassword.equals("")){
            Toast.makeText(RegisterActivity.this, "항목을 모두 입력해주세요.", Toast.LENGTH_SHORT).show();
            return;
        }
        if (!password.equals(checkPassword)){
            Toast.makeText(RegisterActivity.this, "비밀번호가 일치하지 않습니다.", Toast.LENGTH_SHORT).show();
            return;
        }
        RegisterUser(email, password);
    }

    public void RegisterUser(String email, String password){
        firebaseAuth =  FirebaseAuth.getInstance();
        firebaseAuth.createUserWithEmailAndPassword(email, password)
                .addOnCompleteListener(this, new OnCompleteListener<AuthResult>() {
                    @Override
                    public void onComplete(@NonNull Task<AuthResult> task) {
                        if (task.isSuccessful()){
                            FirebaseUser user = firebaseAuth.getCurrentUser();

                            HashMap<Object,String> userInfo = new HashMap<>();
//                            userInfo.put("uid", user.getUid());
                            userInfo.put("email", user.getEmail());
                            userInfo.put("mbti", "");

                            // 회원가입 시 임의로 친구 추가
                            userInfo.put("friend", "hide on bush,CloudTemplar KR,");

                            // firestore cloud database
                            db.collection("users").document(user.getUid())
                                    .set(userInfo)
                                    .addOnFailureListener(new OnFailureListener() {
                                        @Override
                                        public void onFailure(@NonNull Exception e) {
                                            Toast.makeText(RegisterActivity.this, e.toString(), Toast.LENGTH_SHORT).show();
                                        }
                                    });

                            Toast.makeText(RegisterActivity.this, "회원가입이 완료되었습니다.", Toast.LENGTH_SHORT).show();
                            Intent intent = new Intent(getApplication(),TestActivity.class);
                            startActivity(intent);
                        } else{
                            Toast.makeText(RegisterActivity.this, "사용중인 이메일입니다.", Toast.LENGTH_SHORT).show();
                        }
                    }
                });
    }

}