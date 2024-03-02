package com.example.team10;

import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.text.SpannableStringBuilder;
import android.text.Spanned;
import android.text.style.ForegroundColorSpan;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import com.google.android.gms.tasks.OnFailureListener;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.firestore.FirebaseFirestore;

public class ResultActivity extends AppCompatActivity {

    private FirebaseAuth firebaseAuth;
    final FirebaseFirestore db = FirebaseFirestore.getInstance();

    int[] icon = {14,6,10,2,12,4,8,0};
    String[] text = {"일당백 다리우스","농사꾼 나서스", "눈사람 만드는 누누와 윌럼프", "화려한 등장 라칸",
                "조선 제일 검 야스오", "승리를 추구하는 가렌", "천공의 검 레오나", "보호본능 유미"};
    String[] ment = {"\"도망쳐라, 약해빠진 놈들\"!","\"삶과 죽음의 순환은 계속된다. 우리는 살 것이고, 저들은 죽을 것이다.\"",
            "\"모험은 역시 친구랑 같이 해야 신나는 법!\"","\"파티를 시작한다!\"","\"죽음은 바람과 같지. 늘 내 곁에 있으니.\"",
            "\"내 검과 심장은 데마시아의 것이다!\"","\"새벽이 밝았습니다!\"","\"너랑 유미랑! 우리 함께 잘 해보자고!\""};
    int num;
    int[] ic = {R.drawable.darius,R.drawable.nasus,R.drawable.nunu,R.drawable.rakan,R.drawable.yasuo,
            R.drawable.garen,R.drawable.leona,R.drawable.yuumi};

    String test_result; //결과 변수 (캐릭터 이름)

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_result);

        Intent intent = getIntent();
        int res = intent.getIntExtra("result", -1);

        TextView text1 = (TextView)findViewById(R.id.text_re);
        TextView text2 = (TextView)findViewById(R.id.ment);
        ImageView imageView = (ImageView)findViewById(R.id.ima);
        Button button = (Button)findViewById(R.id.btn_main);

        for (int i = 0; i<icon.length; i++){
            if(icon[i] == res){
                num = i;
            }
        }
        test_result = text[num];
        String str = "당신의 LOL플레이 성향은 \n" + test_result + "입니다.";
        SpannableStringBuilder ssb = new SpannableStringBuilder(str);
        ssb.setSpan(new ForegroundColorSpan(Color.parseColor("#ff0000")),16, 16+test_result.length(), Spanned.SPAN_EXCLUSIVE_EXCLUSIVE);

        text1.setText(ssb);
        text2.setText(ment[num]);
        imageView.setImageResource(ic[num]);

        saveResultFirestore(test_result);

        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getApplication(),LoginActivity.class);
                startActivity(intent);
            }
        });

    }

    public void saveResultFirestore(String mbti){
        firebaseAuth =  FirebaseAuth.getInstance();
        FirebaseUser currentUser = firebaseAuth.getCurrentUser();
        String uid = currentUser.getUid();

        db.collection("users").document(uid).update("mbti", mbti).addOnFailureListener(new OnFailureListener() {
            @Override
            public void onFailure(@NonNull Exception e) {
                return;
            }
        });
    }

}
