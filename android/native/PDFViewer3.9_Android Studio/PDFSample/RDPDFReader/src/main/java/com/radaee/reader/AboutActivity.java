package com.radaee.reader;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.widget.Toast;

import com.radaee.pdf.Document;
import com.radaee.pdf.Global;
import com.radaee.pdf.Page;
import com.radaee.util.PDFAssetStream;
import com.radaee.view.PDFLayout;
import com.radaee.view.VPage;

/**
 * Created by radaee on 2015/2/4.
 */
public class AboutActivity extends Activity implements PDFLayoutView.PDFLayoutListener
{
    private PDFLayoutView m_view;
    private Document m_doc;
    private PDFAssetStream m_asset_stream = null;
    private void onFail(String msg)//treat open failed.
    {
        m_doc.Close();
        m_doc = null;
        Toast.makeText(this, msg, Toast.LENGTH_SHORT).show();
        finish();
    }
    private final void ProcessOpenResult(int ret)
    {
        switch( ret )
        {
            case -1://need input password
                onFail("Open Failed: Invalid Password");
                break;
            case -2://unknown encryption
                onFail("Open Failed: Unknown Encryption");
                break;
            case -3://damaged or invalid format
                onFail("Open Failed: Damaged or Invalid PDF file");
                break;
            case -10://access denied or invalid file path
                onFail("Open Failed: Access denied or Invalid path");
                break;
            case 0://succeeded, and continue
                m_view.PDFOpen(m_doc, this);
                break;
            default://unknown error
                onFail("Open Failed: Unknown Error");
                break;
        }
    }
    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        Global.Init(this);
        m_view = new PDFLayoutView(this);
        m_asset_stream = new PDFAssetStream();
        m_asset_stream.open(getAssets(), "About(EN).pdf");
        m_doc = new Document();
        int ret = m_doc.OpenStream(m_asset_stream, null);
        ProcessOpenResult(ret);
        if(ret == 0)
            setContentView(m_view);
    }
    @Override
    protected void onDestroy()
    {
        if(m_view != null) m_view.PDFClose();
        if(m_doc != null) m_doc.Close();
        if(m_asset_stream != null) m_asset_stream.close();
        Global.RemoveTmp();;
        super.onDestroy();
    }

    @Override
    public void OnPDFPageModified(int pageno) {

    }

    @Override
    public void OnPDFPageChanged(int pageno) {

    }

    @Override
    public void OnPDFAnnotTapped(VPage vpage, Page.Annotation annot) {
        m_view.PDFPerformAnnot();
    }

    @Override
    public void OnPDFBlankTapped() {
    }

    @Override
    public void OnPDFSelectEnd(String text) {
    }

    @Override
    public void OnPDFOpenURI(String uri)
    {
        try
        {
            Intent intent = new Intent();
            intent.setAction("android.intent.action.VIEW");
            Uri content_url = Uri.parse(uri);
            intent.setData(content_url);
            startActivity(intent);
        }
        catch(Exception e)
        {
            Toast.makeText(AboutActivity.this, "todo: open url:" + uri, Toast.LENGTH_SHORT).show();
        }
    }

    @Override
    public void OnPDFOpenJS(String js)
    {
    }

    @Override
    public void OnPDFOpenMovie(String path)
    {
    }

    @Override
    public void OnPDFOpenSound(int[] paras, String path) {
    }

    @Override
    public void OnPDFOpenAttachment(String path) {
    }

    @Override
    public void OnPDFOpen3D(String path) {
    }

    @Override
    public void OnPDFZoomStart() {

    }

    @Override
    public void OnPDFZoomEnd() {

    }

    @Override
    public void OnPDFScroll() {

    }

    @Override
    public boolean OnPDFDoubleTapped(PDFLayout layout, float x, float y) {
        return false;
    }
}
