// DirectxForMFC.cpp : ����Ӧ�ó��������Ϊ��
//

#include "stdafx.h"
#include "DirectxForMFC.h"
#include "MainFrm.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CDirectxForMFCApp

BEGIN_MESSAGE_MAP(CDirectxForMFCApp, CWinApp)
	ON_COMMAND(ID_APP_ABOUT, &CDirectxForMFCApp::OnAppAbout)
END_MESSAGE_MAP()


// CDirectxForMFCApp ����

CDirectxForMFCApp::CDirectxForMFCApp()
{
	// TODO: �ڴ˴���ӹ�����룬
	// ��������Ҫ�ĳ�ʼ�������� InitInstance ��
}


// Ψһ��һ�� CDirectxForMFCApp ����

CDirectxForMFCApp theApp;


// CDirectxForMFCApp ��ʼ��

BOOL CDirectxForMFCApp::InitInstance()
{
	// ���һ�������� Windows XP �ϵ�Ӧ�ó����嵥ָ��Ҫ
	// ʹ�� ComCtl32.dll �汾 6 ����߰汾�����ÿ��ӻ���ʽ��
	//����Ҫ InitCommonControlsEx()�����򣬽��޷��������ڡ�
	INITCOMMONCONTROLSEX InitCtrls;
	InitCtrls.dwSize = sizeof(InitCtrls);
	// ��������Ϊ��������Ҫ��Ӧ�ó�����ʹ�õ�
	// �����ؼ��ࡣ
	InitCtrls.dwICC = ICC_WIN95_CLASSES;
	InitCommonControlsEx(&InitCtrls);

	CWinApp::InitInstance();

	// ��ʼ�� OLE ��
	if (!AfxOleInit())
	{
		AfxMessageBox(IDP_OLE_INIT_FAILED);
		return FALSE;
	}
	AfxEnableControlContainer();
	// ��׼��ʼ��
	// ���δʹ����Щ���ܲ�ϣ����С
	// ���տ�ִ���ļ��Ĵ�С����Ӧ�Ƴ�����
	// ����Ҫ���ض���ʼ������
	// �������ڴ洢���õ�ע�����
	// TODO: Ӧ�ʵ��޸ĸ��ַ�����
	// �����޸�Ϊ��˾����֯��
	SetRegistryKey(_T("Ӧ�ó��������ɵı���Ӧ�ó���"));
	// ��Ҫ���������ڣ��˴��뽫�����µĿ�ܴ���
	// ����Ȼ��������ΪӦ�ó���������ڶ���
	CMainFrame* pFrame = new CMainFrame;
	if (!pFrame)
		return FALSE;
	m_pMainWnd = pFrame;
	// ���������ؿ�ܼ�����Դ
	pFrame->LoadFrame(IDR_MAINFRAME,
		WS_OVERLAPPEDWINDOW | FWS_ADDTOTITLE, NULL,
		NULL);






	// Ψһ��һ�������ѳ�ʼ���������ʾ����������и���
	pFrame->ShowWindow(SW_SHOW);
	pFrame->UpdateWindow();
	// �������к�׺ʱ�ŵ��� DragAcceptFiles
	//  �� SDI Ӧ�ó����У���Ӧ�� ProcessShellCommand  ֮����
	return TRUE;
}


// CDirectxForMFCApp ��Ϣ�������




// ����Ӧ�ó��򡰹��ڡ��˵���� CAboutDlg �Ի���

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// �Ի�������
	enum { IDD = IDD_ABOUTBOX };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

// ʵ��
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()

// �������жԻ����Ӧ�ó�������
void CDirectxForMFCApp::OnAppAbout()
{
	CAboutDlg aboutDlg;
	aboutDlg.DoModal();
}


// CDirectxForMFCApp ��Ϣ�������


BOOL CDirectxForMFCApp::OnIdle(LONG lCount)
{
	// TODO: �ڴ����ר�ô����/����û���
	/*CChildView* cview = (CChildView*)m_pMainWnd->GetWindow(GW_CHILD);
	cview->Render();*/
	return CWinApp::OnIdle(lCount);
}

int CDirectxForMFCApp::Run()
{
	// TODO: �ڴ����ר�ô����/����û���
	CChildView* cview = (CChildView*)m_pMainWnd->GetWindow(GW_CHILD);

	MSG msg;
	ZeroMemory( &msg, sizeof( msg ) );
	while(msg.message != WM_QUIT)
	{
		if (::PeekMessage(&msg,NULL,0,0,PM_NOREMOVE))
		{
			if(!AfxGetApp()->PumpMessage())
			{
				::PostQuitMessage(0);
				return false;
			}
		}
		else
		{
			cview->Render();
		}
	}
	return CWinApp::Run();
}
