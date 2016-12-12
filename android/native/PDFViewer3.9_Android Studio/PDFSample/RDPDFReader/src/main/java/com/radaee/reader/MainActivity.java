package com.radaee.reader;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.RelativeLayout;
import android.widget.Toast;

import java.io.File;

public class MainActivity extends Activity implements OnClickListener
{
	private Button m_btn_file;
    private Button m_btn_pager;
	private Button m_btn_asset;
	private Button m_btn_sdcard;
	private Button m_btn_http;
    private Button m_btn_gl;
    private Button m_btn_565;
    private Button m_btn_4444;
    private Button m_btn_curl;
    private Button m_btn_adv;
    private Button m_btn_create;
    private Button m_btn_js;
    private Button m_btn_about;
	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
        //plz set this line to Activity in AndroidManifes.xml:
        //    android:configChanges="orientation|keyboardHidden|screenSize"
        //otherwise, APP shall destroy this Activity and re-create a new Activity when rotate. 
		RelativeLayout layout = (RelativeLayout)LayoutInflater.from(this).inflate(R.layout.activity_main, null);
		m_btn_file = (Button)layout.findViewById(R.id.btn_file);
        m_btn_pager = (Button)layout.findViewById(R.id.btn_pager);
		m_btn_asset = (Button)layout.findViewById(R.id.btn_asset);
		m_btn_sdcard = (Button)layout.findViewById(R.id.btn_sdcard);
		m_btn_http = (Button)layout.findViewById(R.id.btn_http);
        m_btn_gl = (Button)layout.findViewById(R.id.btn_gl);
        m_btn_565 = (Button)layout.findViewById(R.id.btn_565);
        m_btn_4444 = (Button)layout.findViewById(R.id.btn_4444);
        m_btn_curl = (Button)layout.findViewById(R.id.btn_curl);
        m_btn_adv = (Button)layout.findViewById(R.id.btn_advance);
        m_btn_create = (Button)layout.findViewById(R.id.btn_create);
        m_btn_js = (Button)layout.findViewById(R.id.btn_js);
        m_btn_about = (Button)layout.findViewById(R.id.btn_about);
		m_btn_file.setOnClickListener(this);
        m_btn_pager.setOnClickListener(this);
		m_btn_asset.setOnClickListener(this);
		m_btn_sdcard.setOnClickListener(this);
		m_btn_http.setOnClickListener(this);
        m_btn_gl.setOnClickListener(this);
        m_btn_565.setOnClickListener(this);
        m_btn_4444.setOnClickListener(this);
        m_btn_curl.setOnClickListener(this);
        m_btn_adv.setOnClickListener(this);
        m_btn_create.setOnClickListener(this);
        m_btn_js.setOnClickListener(this);
        m_btn_about.setOnClickListener(this);
		setContentView(layout);
	}
    @SuppressLint("InlinedApi")
	@Override
    protected void onDestroy()
    {
    	com.radaee.pdf.Global.RemoveTmp();
    	super.onDestroy();
    }
	@Override
	public void onClick(View arg0)
	{
		if( arg0 == m_btn_file )
		{
			Intent intent = new Intent(this, com.radaee.reader.PDFNavAct.class);
			startActivity(intent);
		}
        else if( arg0 == m_btn_pager )
        {
            Intent intent = new Intent(this, com.radaee.reader.PDFPagerAct.class);
            intent.putExtra( "PDFAsset", "test.PDF" );
            intent.putExtra( "PDFPswd", "" );//password
            startActivity(intent);
        }
		else if( arg0 == m_btn_asset )
		{
			Intent intent = new Intent(this, com.radaee.reader.PDFViewAct.class);
			intent.putExtra( "PDFAsset", "test.PDF" );
			intent.putExtra( "PDFPswd", "" );//password
			startActivity(intent);
		}
		else if( arg0 == m_btn_sdcard )
		{
			Intent intent = new Intent(this, com.radaee.reader.PDFViewAct.class);
			String pdf_path = "/sdcard/test.pdf";
			File file = new File(pdf_path);
			if( file.exists() )
			{
				intent.putExtra( "PDFPath", pdf_path );
				intent.putExtra( "PDFPswd", "" );//password
				startActivity(intent);
			}
			else
			{
				Toast.makeText(this, "file not exists:" + pdf_path, Toast.LENGTH_SHORT).show();
			}
		}
		else if( arg0 == m_btn_http )
		{
			Intent intent = new Intent(this, com.radaee.reader.PDFPagerAct.class);
			String http_link = "http://www.radaeepdf.com/documentation/MRBrochoure.pdf";
            //String http_link = "http://www.radaeepdf.com/documentation/oversize_pdf_test_0_opt.pdf";
            //String http_link = "http://www.anydomain.com/someWorkingPDFURL.pdf";
			intent.putExtra( "PDFHttp", http_link );
			intent.putExtra( "PDFPswd", "" );//password
			startActivity(intent);
		}
        else if( arg0 == m_btn_gl )
        {
            Intent intent = new Intent(this, PDFGLViewAct.class);
            startActivity(intent);
        }
        else if( arg0 == m_btn_565 )
        {
            Intent intent = new Intent(this, com.radaee.reader.PDFViewAct.class);
            intent.putExtra( "PDFAsset", "test.PDF" );
            intent.putExtra( "PDFPswd", "" );//password
            intent.putExtra( "BMPFormat", "RGB_565" );
            startActivity(intent);
        }
        else if( arg0 == m_btn_4444 )
        {
            Intent intent = new Intent(this, com.radaee.reader.PDFViewAct.class);
            intent.putExtra( "PDFAsset", "test.PDF" );
            intent.putExtra( "PDFPswd", "" );//password
            intent.putExtra( "BMPFormat", "ARGB_4444" );
            startActivity(intent);
        }
        else if( arg0 == m_btn_curl)
        {
            Intent intent = new Intent(this, com.radaee.reader.PDFCurlViewAct.class);
            intent.putExtra( "PDFAsset", "test.PDF" );
            intent.putExtra( "PDFPswd", "" );//password
            startActivity(intent);
        }
        else if( arg0 == m_btn_adv)
        {
            Intent intent = new Intent(this, AdvanceAct.class);
            startActivity(intent);
        }
        else if( arg0 == m_btn_create )
        {
            Intent intent = new Intent(this, PDFTestAct.class);
            startActivity(intent);
        }
        else if( arg0 == m_btn_js)
        {
            Intent intent = new Intent(this, PDFJSTestAct.class);
            startActivity(intent);
        }
        else if( arg0 == m_btn_about )
        {
            Intent intent = new Intent(this, AboutActivity.class);
            startActivity(intent);
        }
	}
}
