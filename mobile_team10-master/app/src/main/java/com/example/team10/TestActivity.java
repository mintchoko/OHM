package com.example.team10;

import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

public class TestActivity extends AppCompatActivity {
    FragmentManager fm;
    FragmentTransaction tran;
    Frag1 f1;
    Boolean[] bool = new Boolean[2]; //조사 결과 저장

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_test);

        f1 = new Frag1();

        fm = getSupportFragmentManager();
        tran = fm.beginTransaction();
        tran.replace(R.id.main, f1);
        tran.commit();
    }


}