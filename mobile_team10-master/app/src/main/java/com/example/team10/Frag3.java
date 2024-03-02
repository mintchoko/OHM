package com.example.team10;

import android.content.Context;
import android.content.Intent;
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

public class Frag3 extends Fragment {
    public static Frag3 newInstance() {
        return new Frag3();
    }

    TestActivity main;
    int count, itn;
    boolean re1,re2,re3;
    FragmentTransaction tran;
    Frag2 frag2;

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
        View view = inflater.inflate(R.layout.frag3, container, false);
        Button b3_b = (Button)view.findViewById(R.id.bt3_back);
        Button btr = (Button)view.findViewById(R.id.bt_re);
        RadioGroup rg11 = (RadioGroup)view.findViewById(R.id.question_11);
        RadioGroup rg12 = (RadioGroup)view.findViewById(R.id.question_12);
        RadioGroup rg13 = (RadioGroup)view.findViewById(R.id.question_13);

        tran = getActivity().getSupportFragmentManager().beginTransaction();
        frag2 = new Frag2();

        b3_b.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                tran.replace(R.id.main, frag2);
                tran.commit();
            }
        });

        btr.setOnClickListener(new View.OnClickListener() {
            @RequiresApi(api = Build.VERSION_CODES.N)
            @Override
            public void onClick(View view) {
                int rs11,rs12,rs13;
                rs11 = rg11.getCheckedRadioButtonId();
                rs12 = rg12.getCheckedRadioButtonId();
                rs13 = rg13.getCheckedRadioButtonId();
                int[] rs = {rs11, rs12, rs13,};



                if(IntStream.of(rs).anyMatch(i -> i == -1)){
                    Toast.makeText(getActivity(), "모든 항목에 체크해주세요.", Toast.LENGTH_SHORT).show();
                }
                else{
                    if (rs11 == R.id.q_11_1){
                        count += 1;
                    }
                    else{
                        count -= 1;
                    }

                    if (rs12 == R.id.q_12_1){
                        count += 1;
                    }
                    else{
                        count -= 1;
                    }

                    if (rs13 == R.id.q_13_1){
                        count += 1;
                    }
                    else{
                        count -= 1;
                    }

                    if (count > 0){
                        re3 = true;
                    }
                    else{
                        re3 = false;
                    }

                    itn = 0;

                    re1 = main.bool[0];
                    re2 = main.bool[1];

                    Boolean[] res = {re1,re2,re3};
                    for(int s = 0; s < res.length; s++){
                        if(res[s]){
                            itn += (int)Math.pow(2,s+1);
                        }
                    }

                    Intent intent = new Intent(getActivity(),ResultActivity.class);
                    intent.putExtra("result",itn);
                    startActivity(intent);
                }
            }
        });

        return view;
    }

}
