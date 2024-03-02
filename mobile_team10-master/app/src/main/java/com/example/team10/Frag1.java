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

public class Frag1 extends Fragment {
    public static Frag1 newInstance() {
        return new Frag1();
    }

    TestActivity main;
    int count;
    boolean re1;

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
        View view = inflater.inflate(R.layout.frag1, container, false);
        Button b1 = (Button)view.findViewById(R.id.bt1_next);
        RadioGroup rg1 = (RadioGroup)view.findViewById(R.id.question_1);
        RadioGroup rg2 = (RadioGroup)view.findViewById(R.id.question_2);
        RadioGroup rg3 = (RadioGroup)view.findViewById(R.id.question_3);
        RadioGroup rg4 = (RadioGroup)view.findViewById(R.id.question_4);
        RadioGroup rg5 = (RadioGroup)view.findViewById(R.id.question_5);

        b1.setOnClickListener(new View.OnClickListener() {
            @RequiresApi(api = Build.VERSION_CODES.N)
            @Override
            public void onClick(View view) {
                int rs1,rs2,rs3,rs4,rs5;
                rs1 = rg1.getCheckedRadioButtonId();
                rs2 = rg2.getCheckedRadioButtonId();
                rs3 = rg3.getCheckedRadioButtonId();
                rs4 = rg4.getCheckedRadioButtonId();
                rs5 = rg5.getCheckedRadioButtonId();
                int[] rs = {rs1, rs2, rs3, rs4, rs5};

                count = 0;

                if(IntStream.of(rs).anyMatch(i -> i == -1)){
                    Toast.makeText(getActivity(), "모든 항목에 체크해주세요.", Toast.LENGTH_SHORT).show();
                }
                else{
                    if (rs1 == R.id.q_1_1){
                        count += 1;
                    }
                    else{
                        count -= 1;
                    }

                    if (rs2 == R.id.q_2_1){
                        count += 1;
                    }
                    else{
                        count -= 1;
                    }

                    if (rs3 == R.id.q_3_2){
                        count += 1;
                    }
                    else{
                        count -= 1;
                    }

                    if (rs4 == R.id.q_4_2){
                        count += 1;
                    }
                    else{
                        count -= 1;
                    }

                    if (rs5 == R.id.q_5_2){
                        count += 1;
                    }
                    else{
                        count -= 1;
                    }

                    if (count > 0){
                        re1 = true;
                    }
                    else{
                        re1 = false;
                    }

                    main.bool[0] = re1;

                    FragmentTransaction transaction = getActivity().getSupportFragmentManager().beginTransaction();
                    Frag2 frag2 = new Frag2();
                    transaction.replace(R.id.main, frag2);
                    transaction.commit();
                }
            }
        });

        return view;
    }

}
