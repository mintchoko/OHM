package com.example.team10;

import android.content.Context;
import android.os.Build;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.RadioGroup;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.RequiresApi;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;

import java.util.stream.IntStream;

public class Frag2 extends Fragment {
    public static Frag2 newInstance() {
        return new Frag2();
    }

    TestActivity main;
    int count;
    boolean re2;
    FragmentTransaction tran;
    Frag1 frag1;

    public void onAttach(@NonNull Context context){
        super.onAttach(context);

        main = (TestActivity) getActivity();
    }

    public void onDetach(){
        super.onDetach();

        main = null;
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState){
        View view = inflater.inflate(R.layout.frag2, container, false);
        Button b2_b = (Button)view.findViewById(R.id.bt2_back);
        Button b2_n = (Button)view.findViewById(R.id.bt2_next);
        RadioGroup rg6 = (RadioGroup)view.findViewById(R.id.question_6);
        RadioGroup rg7 = (RadioGroup)view.findViewById(R.id.question_7);
        RadioGroup rg8 = (RadioGroup)view.findViewById(R.id.question_8);
        RadioGroup rg9 = (RadioGroup)view.findViewById(R.id.question_9);
        RadioGroup rg10 = (RadioGroup)view.findViewById(R.id.question_10);
        tran = getActivity().getSupportFragmentManager().beginTransaction();
        frag1 = new Frag1();

        b2_b.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                tran.replace(R.id.main, frag1);
                tran.commit();
            }
        });


        b2_n.setOnClickListener(new View.OnClickListener() {
            @RequiresApi(api = Build.VERSION_CODES.N)
            @Override
            public void onClick(View view) {
                int rs6,rs7,rs8,rs9,rs10;
                rs6 = rg6.getCheckedRadioButtonId();
                rs7 = rg7.getCheckedRadioButtonId();
                rs8 = rg8.getCheckedRadioButtonId();
                rs9 = rg9.getCheckedRadioButtonId();
                rs10 = rg10.getCheckedRadioButtonId();
                int[] rs = {rs6, rs7, rs8, rs9, rs10};

                if(IntStream.of(rs).anyMatch(i -> i == -1)){
                    Toast.makeText(getActivity(), "모든 항목에 체크해주세요.", Toast.LENGTH_SHORT).show();
                }
                else{
                    if (rs6 == R.id.q_6_1){
                        count += 1;
                    }
                    else{
                        count -= 1;
                    }

                    if (rs7 == R.id.q_7_1){
                        count += 1;
                    }
                    else{
                        count -= 1;
                    }

                    if (rs8 == R.id.q_8_1){
                        count += 1;
                    }
                    else{
                        count -= 1;
                    }

                    if (rs9 == R.id.q_9_2){
                        count += 1;
                    }
                    else{
                        count -= 1;
                    }

                    if (rs10 == R.id.q_10_1){
                        count += 1;
                    }
                    else{
                        count -= 1;
                    }

                    if (count > 0){
                        re2 = true;
                    }
                    else{
                        re2 = false;
                    }

                    main.bool[1] = re2;

                    FragmentTransaction transaction = getActivity().getSupportFragmentManager().beginTransaction();
                    Frag3 frag3 = new Frag3();
                    transaction.replace(R.id.main, frag3);
                    transaction.commit();
                }
            }
        });

        return view;

    }

}
