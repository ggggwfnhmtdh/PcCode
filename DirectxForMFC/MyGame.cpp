#include "StdAfx.h"
#include "MyGame.h"

CMyGame::CMyGame(LPDIRECT3DDEVICE9  pd3dDevice)
{
	m_pd3dDevice=pd3dDevice;
	CUSTOMVERTEX g_Vertices[] =
    {
        { -1.0f,-1.0f, 0.0f, 0xffff0000, },
        {  1.0f,-1.0f, 0.0f, 0xff0000ff, },
        {  0.0f, 1.0f, 0.0f, 0xffffffff, },
    };

    // Create the vertex buffer.
    if( FAILED( m_pd3dDevice->CreateVertexBuffer( 3 * sizeof( CUSTOMVERTEX ),
                                                  0, D3DFVF_CUSTOMVERTEX,
                                                  D3DPOOL_DEFAULT, &g_pVB, NULL ) ) )
    {
        return;
    }

    // Fill the vertex buffer.
    VOID* pVertices;
    if( FAILED( g_pVB->Lock( 0, sizeof( g_Vertices ), ( void** )&pVertices, 0 ) ) )
        return;
    memcpy( pVertices, g_Vertices, sizeof( g_Vertices ) );
    g_pVB->Unlock();

}

void CMyGame::Render(float timeDre)
{
	D3DXMATRIXA16 matWorld;

	// Set up the rotation matrix to generate 1 full rotation (2*PI radians) 
	// every 1000 ms. To avoid the loss of precision inherent in very high 
	// floating point numbers, the system time is modulated by the rotation 
	// period before conversion to a radian angle.
	UINT iTime = timeGetTime() % 1000;
	FLOAT fAngle = iTime * ( 2.0f * D3DX_PI ) / 1000.0f;
	D3DXMatrixRotationY( &matWorld, fAngle );
	m_pd3dDevice->SetTransform( D3DTS_WORLD, &matWorld );

	m_pd3dDevice->SetStreamSource( 0, g_pVB, 0, sizeof( CUSTOMVERTEX ) );
	m_pd3dDevice->SetFVF( D3DFVF_CUSTOMVERTEX );
	m_pd3dDevice->DrawPrimitive( D3DPT_TRIANGLESTRIP, 0, 1 );
}
CMyGame::~CMyGame(void)
{
	if( g_pVB != NULL )
		g_pVB->Release();

}
