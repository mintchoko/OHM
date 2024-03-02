package com.example.team10;

import android.os.Bundle;
import android.util.Log;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.FragmentActivity;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import com.google.android.material.bottomnavigation.BottomNavigationView;
import com.google.firebase.firestore.auth.User;

public class FragActivity extends AppCompatActivity {
    FriendActivity friendActivity;
    MainActivity mainActivity;
    UserActivity userActivity;

    private FragmentManager fragmentManager;
    private FragmentTransaction fragmentTransaction;

    int present_fragment = 3;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_frag);

        fragmentManager = getSupportFragmentManager();
        fragmentTransaction = fragmentManager.beginTransaction();


        mainActivity = (MainActivity) fragmentManager.findFragmentById(R.id.fragment);
        fragmentTransaction.commit();
        friendActivity = new FriendActivity();
        userActivity = new UserActivity();
        //하단 네비게이션 메뉴의 아이템을 누를 때마다 해당 동작을 실행한다.
        BottomNavigationView bottomNavigationView = (BottomNavigationView) findViewById(R.id.bottomNavigationView);
        bottomNavigationView.setOnItemSelectedListener(item -> {
            switch (item.getItemId()){
                case R.id.item_friend:
                    onFragmentChanged(1);
                    break;
                case R.id.item_main:
                    onFragmentChanged(0);
                    break;
                case R.id.item_info:
                    onFragmentChanged(2);
                    break;
            }
            return true;
        });



    }
    public void onFragmentChanged(int index) {

        fragmentManager = getSupportFragmentManager();
        fragmentTransaction = fragmentManager.beginTransaction();
        if (index == 0) {
            fragmentTransaction.replace(R.id.container, mainActivity);
            fragmentTransaction.commit();
        } else if (index == 1) {
            fragmentTransaction.replace(R.id.container, friendActivity);
            fragmentTransaction.commit();
        } else if (index == 2) {
            fragmentTransaction.replace(R.id.container, userActivity);
            fragmentTransaction.commit();
        }
        present_fragment = index;
    }
}
