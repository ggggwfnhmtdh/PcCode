// ChildView.cpp : CChildView 类的实现
//

#include "stdafx.h"
#include "DirectxForMFC.h"
#include "ChildView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CChildView

CChildView::CChildView()
{
	m_pD3D=NULL;
	m_pd3dDevice=NULL;
	
}

CChildView::~CChildView()
{
	if( m_pd3dDevice != NULL )
		m_pd3dDevice->Release();

	if( m_pD3D != NULL )
		m_pD3D->Release();
}


BEGIN_MESSAGE_MAP(CChildView, CWnd)
	ON_WM_PAINT()
	ON_WM_CREATE()
	ON_WM_KEYDOWN()
END_MESSAGE_MAP()



// CChildView 消息处理程序

BOOL CChildView::PreCreateWindow(CREATESTRUCT& cs) 
{
	if (!CWnd::PreCreateWindow(cs))
		return FALSE;

	//去除内边框
	//cs.dwExStyle |= WS_EX_CLIENTEDGE;
	cs.style &= ~WS_BORDER;
	cs.lpszClass = AfxRegisterWndClass(CS_HREDRAW|CS_VREDRAW|CS_DBLCLKS, 
		::LoadCursor(NULL, IDC_ARROW), reinterpret_cast<HBRUSH>(COLOR_WINDOW+1), NULL);

	return TRUE;
}

void CChildView::OnPaint() 
{
	CPaintDC dc(this); // 用于绘制的设备上下文
	
	// TODO: 在此处添加消息处理程序代码
	
	// 不要为绘制消息而调用 CWnd::OnPaint()
}

HRESULT CChildView::InitD3D( HWND hWnd ,int Width,int Height)
{
	// Create the D3D object.
	if( NULL == ( m_pD3D = Direct3DCreate9( D3D_SDK_VERSION ) ) )
		return E_FAIL;

	// Set up the structure used to create the D3DDevice
	D3DPRESENT_PARAMETERS d3dpp;
	ZeroMemory( &d3dpp, sizeof(d3dpp) );
	d3dpp.Windowed = TRUE;
	d3dpp.SwapEffect = D3DSWAPEFFECT_DISCARD;
	d3dpp.BackBufferFormat = D3DFMT_UNKNOWN;
	d3dpp.BackBufferWidth=Width;
	d3dpp.BackBufferHeight=Height;

	// Create the D3DDevice
	if( FAILED( m_pD3D->CreateDevice( D3DADAPTER_DEFAULT, D3DDEVTYPE_HAL, hWnd,
		D3DCREATE_SOFTWARE_VERTEXPROCESSING,
		&d3dpp, &m_pd3dDevice ) ) )
	{
		return E_FAIL;
	}

	// Turn off culling, so we see the front and back of the triangle
	m_pd3dDevice->SetRenderState( D3DRS_CULLMODE, D3DCULL_NONE );

	// Turn off D3D lighting, since we are providing our own vertex colors
	m_pd3dDevice->SetRenderState( D3DRS_LIGHTING, FALSE );

	return S_OK;
}

float CChildView::GetElaspedTime()
{
	static float timeNow=timeGetTime()/1000.0f;
	static float timePre=0;
	timePre=timeNow;
	timeNow=timeGetTime()/1000.0f;
	float timeElasped=timeNow-timePre;
	return timeElasped;
}

void CChildView::Update()
{
	// Set up our view matrix. A view matrix can be defined given an eye point,
	// a point to lookat, and a direction for which way is up. Here, we set the
	// eye five units back along the z-axis and up three units, look at the
	// origin, and define "up" to be in the y-direction.
	D3DXVECTOR3 vEyePt( 0.0f, 3.0f,-5.0f );
	D3DXVECTOR3 vLookatPt( 0.0f, 0.0f, 0.0f );
	D3DXVECTOR3 vUpVec( 0.0f, 1.0f, 0.0f );
	D3DXMATRIXA16 matView;
	D3DXMatrixLookAtLH( &matView, &vEyePt, &vLookatPt, &vUpVec );
	m_pd3dDevice->SetTransform( D3DTS_VIEW, &matView );

	// For the projection matrix, we set up a perspective transform (which
	// transforms geometry from 3D view space to 2D viewport space, with
	// a perspective divide making objects smaller in the distance). To build
	// a perpsective transform, we need the field of view (1/4 pi is common),
	// the aspect ratio, and the near and far clipping planes (which define at
	// what distances geometry should be no longer be rendered).
	D3DXMATRIXA16 matProj;
	D3DXMatrixPerspectiveFovLH( &matProj, D3DX_PI/4, 1.0f, 1.0f, 100.0f );
	m_pd3dDevice->SetTransform( D3DTS_PROJECTION, &matProj );
}

void CChildView::Render()
{
	float timeDelta = GetElaspedTime();

	m_pd3dDevice->Clear( 0, NULL, D3DCLEAR_TARGET, D3DCOLOR_XRGB(0,128,128), 1.0f, 0 );
	
	// Begin the scene
	if( SUCCEEDED( m_pd3dDevice->BeginScene() ) )
	{
		// Setup the world, view, and projection Matrices
		Update();
		
		m_game->Render(timeDelta);
		

		// End the scene
		m_pd3dDevice->EndScene();
	}

	// Present the backbuffer contents to the display
	m_pd3dDevice->Present( NULL, NULL, NULL, NULL );
}

int CChildView::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CWnd::OnCreate(lpCreateStruct) == -1)
		return -1;

	// TODO:  在此添加您专用的创建代码
	InitD3D(m_hWnd,400,400);
	

	m_game=new CMyGame(m_pd3dDevice);

	return 0;
}

void CChildView::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值

	switch(nChar)
	{
	case VK_F1:
		m_dlg.DoModal();
		break;
	case VK_ESCAPE:
		PostQuitMessage(0);
		break;
	}

	CWnd::OnKeyDown(nChar, nRepCnt, nFlags);
}
