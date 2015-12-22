
// MFCApplication1Dlg.cpp : implementation file
//

#include "stdafx.h"
#include "MFCApplication1.h"
#include "MFCApplication1Dlg.h"
#include "afxdialogex.h"
#include <d3d9.h>
#include<WinUser.h>
#pragma warning( disable : 4996 ) // disable deprecated warning 
#include <strsafe.h>
#pragma warning( default : 4996 )




//-----------------------------------------------------------------------------
// Global variables
//-----------------------------------------------------------------------------
LPDIRECT3D9         g_pD3D = NULL; // Used to create the D3DDevice
LPDIRECT3DDEVICE9   g_pd3dDevice = NULL; // Our rendering device

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CAboutDlg dialog used for App About

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
	ON_BN_CLICKED(IDOK, &CAboutDlg::OnBnClickedOk)
END_MESSAGE_MAP()


// CMFCApplication1Dlg dialog



CMFCApplication1Dlg::CMFCApplication1Dlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CMFCApplication1Dlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMFCApplication1Dlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_WEB_TREE, m_tree);
}

BEGIN_MESSAGE_MAP(CMFCApplication1Dlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDOK, &CMFCApplication1Dlg::OnBnClickedOk)
	ON_BN_CLICKED(IDC_BUTTON1, &CMFCApplication1Dlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON2, &CMFCApplication1Dlg::OnBnClickedButton2)
	ON_BN_CLICKED(IDCANCEL, &CMFCApplication1Dlg::OnBnClickedCancel)
END_MESSAGE_MAP()


// CMFCApplication1Dlg message handlers

BOOL CMFCApplication1Dlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here
	m_tree.ModifyStyle(0, TVS_HASLINES | TVS_LINESATROOT | TVS_HASBUTTONS | TVS_EDITLABELS | TVS_SHOWSELALWAYS);
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CMFCApplication1Dlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CMFCApplication1Dlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CMFCApplication1Dlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


int High = 0;
void CMFCApplication1Dlg::OnBnClickedOk()
{
	TVINSERTSTRUCT tvInsert;
	tvInsert.hParent = NULL;
	tvInsert.hInsertAfter = NULL;
	tvInsert.item.mask = TVIF_TEXT;
	tvInsert.item.pszText  = _T("Node0");
	HTREEITEM pNode0 = m_tree.InsertItem(&tvInsert);

	tvInsert.hParent = NULL;
	tvInsert.hInsertAfter = NULL;
	tvInsert.item.mask = TVIF_TEXT;
	tvInsert.item.pszText = _T("Node1");
	HTREEITEM pNode1 = m_tree.InsertItem(&tvInsert);

	tvInsert.hParent = pNode0;
	tvInsert.hInsertAfter = NULL;
	tvInsert.item.mask = TVIF_TEXT;
	tvInsert.item.pszText = _T("Node01");
	HTREEITEM pNode01 = m_tree.InsertItem(&tvInsert);
}

void CMFCApplication1Dlg::ArrayShowPic(int x,int y,unsigned char*data)
{
	CWnd *pWin = GetDlgItem(IDC_PIC_SHOW);//获取该控件的指针，就可以对该控件直接操作了
	CDC *pdc = pWin->GetDC();//获取该控件的画布
	CDC memdc;
	CBitmap bmp;
	int Data = 0;
	int PixelNum = x* y;
	CString str;
	BYTE *pData = new BYTE[PixelNum * 4];        // show in 32 bits
	long t1 = GetTickCount();
	for (int i = 0; i < PixelNum; i++)
	{
		pData[i * 4 + 0] = 0;    //Blue
		pData[i * 4 + 1] = 0;    //Green
		pData[i * 4 + 2] = data[i];    //Red
		pData[i * 4 + 3] = 0;
	}
	memdc.CreateCompatibleDC(pdc);
	bmp.CreateBitmap(x, y, 1, 32, pData);
	CBitmap* pOldBitmap = memdc.SelectObject(&bmp);
	pdc->BitBlt(0, 0, x, y, &memdc, 0, 0, SRCCOPY);
	memdc.SelectObject(pOldBitmap);
	memdc.DeleteDC();
	ReleaseDC(pdc);
	delete[] pData;
}

void CMFCApplication1Dlg::ShowPress(int Value, int Max)
{
	unsigned char NumData[10][32] =
	{
		0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x7, 0xF0, 0x1C, 0x18, 0x38, 0x1C, 0x38, 0xE, 0x38, 0xE, 0x78, 0xE, 0x38, 0xE, 0x38, 0xE, 0x38, 0x1C, 0x1C, 0x38, 0x7, 0xF0, 0x0, 0x0, 0x0, 0x0,
		0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x3, 0xC0, 0xF, 0xC0, 0x1, 0xC0, 0x1, 0xC0, 0x1, 0xC0, 0x1, 0xC0, 0x1, 0xC0, 0x1, 0xC0, 0x1, 0xC0, 0x1, 0xC0, 0xF, 0xF8, 0x0, 0x0, 0x0, 0x0,
		0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xF, 0xF8, 0x18, 0x1C, 0x38, 0xC, 0x18, 0x1C, 0x0, 0x38, 0x0, 0x70, 0x1, 0xC0, 0x7, 0x0, 0x1C, 0x4, 0x3F, 0xFC, 0x3F, 0xFC, 0x0, 0x0, 0x0, 0x0,
		0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xF, 0xF0, 0x38, 0x18, 0x38, 0x1C, 0x0, 0x18, 0x1, 0xF0, 0x1, 0xF8, 0x0, 0x1C, 0x0, 0xE, 0x38, 0xC, 0x38, 0x1C, 0xF, 0xF0, 0x0, 0x0, 0x0, 0x0,
		0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x30, 0x0, 0xF0, 0x1, 0xF0, 0x2, 0x70, 0xC, 0x70, 0x18, 0x70, 0x30, 0x70, 0x3F, 0xFE, 0x0, 0x70, 0x0, 0x70, 0x3, 0xFE, 0x0, 0x0, 0x0, 0x0,
		0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1F, 0xFC, 0x18, 0x0, 0x18, 0x0, 0x18, 0xC0, 0x1F, 0xF8, 0x18, 0x1C, 0x0, 0xC, 0x18, 0xE, 0x38, 0xC, 0x38, 0x18, 0xF, 0xF0, 0x0, 0x0, 0x0, 0x0,
		0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x7, 0xF8, 0xC, 0x1C, 0x18, 0x0, 0x38, 0x0, 0x3F, 0xF8, 0x3C, 0x1C, 0x38, 0xE, 0x38, 0xE, 0x38, 0xE, 0x1C, 0x1C, 0x7, 0xF0, 0x0, 0x0, 0x0, 0x0,
		0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1F, 0xFE, 0x38, 0xC, 0x30, 0x10, 0x0, 0x20, 0x0, 0x60, 0x0, 0xC0, 0x1, 0x80, 0x3, 0x80, 0x3, 0x80, 0x3, 0x80, 0x3, 0x80, 0x0, 0x0, 0x0, 0x0,
		0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xF, 0xF8, 0x38, 0xC, 0x30, 0xC, 0x3C, 0xC, 0xF, 0xF8, 0xF, 0xF0, 0x38, 0x3C, 0x30, 0xE, 0x70, 0xE, 0x38, 0x1C, 0xF, 0xF0, 0x0, 0x0, 0x0, 0x0,
		0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xF, 0xF0, 0x38, 0x18, 0x30, 0xC, 0x70, 0xE, 0x30, 0x1E, 0x38, 0x3E, 0xF, 0xEE, 0x0, 0x1C, 0x18, 0x18, 0x3C, 0x30, 0x1F, 0xE0, 0x0, 0x0, 0x0, 0x0,
	};
	unsigned char Data;
	CWnd *pWin = GetDlgItem(IDC_PIC_SHOW);//获取该控件的指针，就可以对该控件直接操作了
	CDC *pdc = pWin->GetDC();//获取该控件的画布
	CDC memdc;
	CBitmap bmp;
	int High;
	int x = 100, y = 200,StartRow,StartCol,CurNum[3];
	int PixelNum = x* y;
	BYTE *pData = new BYTE[PixelNum * 4];        // show in 32 bits
	if (Value < 0)
		Value = 0;
	if (Value > Max)
		Value = Max;
	Data = ((double)Value / Max) * 255;
	High = y - ((double)Value / Max) * y;
	for (int i = 0; i < High*100; i++)
	{
		pData[i * 4 + 0] = 0;    //Blue
		pData[i * 4 + 1] = 0;    //Green
		pData[i * 4 + 2] = 0;    //Red
		pData[i * 4 + 3] = 0;
	}
	for (int i = High * 100; i < PixelNum; i++)
	{
		pData[i * 4 + 0] = 0;    //Blue
		pData[i * 4 + 1] = 0;    //Green
		pData[i * 4 + 2] = Data;    //Red
		pData[i * 4 + 3] = 0;
	}

	StartRow = y / 2 - 24;
	StartCol = x / 2 - 24;
	CurNum[0] = Value/100;
	CurNum[1] = (Value / 10)%10;
	CurNum[2] = Value % 10;
	for (int m = 0; m < 3; m++)
	{
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				int Index = (i * 16 + j) / 8;;
				int BitNum = (i * 16 + j) % 8;
				int PixelOffset = (StartRow + i)*x + StartCol + j+m*16;
				if (NumData[CurNum[m]][Index] & (0x80 >> BitNum))
				{
					pData[PixelOffset * 4 + 0] = 0;    //Blue
					pData[PixelOffset * 4 + 1] = 250;    //Green
					pData[PixelOffset * 4 + 2] = 0;    //Red
					pData[PixelOffset * 4 + 3] = 0;
				}
			}
		}
	}

	memdc.CreateCompatibleDC(pdc);
	bmp.CreateBitmap(x, y, 1, 32, pData);
	CBitmap* pOldBitmap = memdc.SelectObject(&bmp);
	pdc->BitBlt(0, 0, x, y, &memdc, 0, 0, SRCCOPY);
	memdc.SelectObject(pOldBitmap);
	memdc.DeleteDC();
	ReleaseDC(pdc);
	delete[] pData;
}

void CMFCApplication1Dlg::ShowCirCle(int x,int y,int R)
{

	CWnd *pWin = GetDlgItem(IDC_PIC_SHOW);//获取该控件的指针，就可以对该控件直接操作了
	CDC *pdc = pWin->GetDC();//获取该控件的画布
	CDC memdc;
	CBitmap bmp;
	int Width = 200, High = 200;
	int PixelNum = Width* High;
	BYTE *pData = new BYTE[PixelNum * 4];        // show in 32 bits
	static BYTE Data = 128;
	Data = Data+10;
	for (int i = 0; i < PixelNum; i++)
	{
		int Row = i / Width;
		int Col = i%Width;
		if (((Row-x)*(Row-x) + (Col-y)*(Col-y)) <= R*R)
		{
			pData[i * 4 + 0] = 203;    //Blue
			pData[i * 4 + 1] = 192;    //Green
			pData[i * 4 + 2] = 255;    //Red
			pData[i * 4 + 3] = 0;
		}
		else
		{
			pData[i * 4 + 0] = 0;    //Blue
			pData[i * 4 + 1] = 0;    //Green
			pData[i * 4 + 2] = 0;    //Red
			pData[i * 4 + 3] = 0;
		}

	}

	memdc.CreateCompatibleDC(pdc);
	bmp.CreateBitmap(Width, High, 1, 32, pData);
	CBitmap* pOldBitmap = memdc.SelectObject(&bmp);
	pdc->BitBlt(0, 0, Width, High, &memdc, 0, 0, SRCCOPY);
	memdc.SelectObject(pOldBitmap);
	memdc.DeleteDC();
	ReleaseDC(pdc);
	delete[] pData;
}

HRESULT InitD3D(HWND hWnd)
{
	// Create the D3D object.
	if (NULL == (g_pD3D = Direct3DCreate9(D3D_SDK_VERSION)))
		return E_FAIL;

	// Set up the structure used to create the D3DDevice
	D3DPRESENT_PARAMETERS d3dpp;
	ZeroMemory(&d3dpp, sizeof(d3dpp));
	d3dpp.Windowed = TRUE;
	d3dpp.SwapEffect = D3DSWAPEFFECT_DISCARD;
	d3dpp.BackBufferFormat = D3DFMT_UNKNOWN;

	// Create the D3DDevice
	if (FAILED(g_pD3D->CreateDevice(D3DADAPTER_DEFAULT, D3DDEVTYPE_HAL, GetDesktopWindow(),
		D3DCREATE_SOFTWARE_VERTEXPROCESSING,
		&d3dpp, &g_pd3dDevice)))
	{
		return E_FAIL;
	}

	// Device state would normally be set here

	return S_OK;
}

void CMFCApplication1Dlg::OnBnClickedButton1()
{
	/*CWnd *pWnd = GetDlgItem(IDC_PIC_SHOW);
	HWND hwnd = pWnd->GetSafeHwnd();
	InitD3D(hwnd);*/
	int Press = 0;
	while (1)
	{
		ShowPress(Press++, 500);
		Sleep(10);
		if (Press > 500)
			break;
	}
}



void CMFCApplication1Dlg::OnBnClickedButton2()
{
	    High = High - 20;
		OnBnClickedOk();
}


void CMFCApplication1Dlg::OnBnClickedCancel()
{
	CString FilePath = _T("D:\\123.txt");
	CStdioFile  File;
	File.Open(FilePath, CFile::modeCreate | CFile::modeReadWrite);
	File.WriteString(_T("123\n456"));
	File.Close();
	ShellExecute(NULL, _T("open"), FilePath, NULL, NULL, SW_SHOW);
}


void CAboutDlg::OnBnClickedOk()
{
	// TODO: Add your control notification handler code here
	CDialogEx::OnOK();
}
