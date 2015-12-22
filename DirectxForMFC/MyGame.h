#pragma once
#define D3DFVF_CUSTOMVERTEX (D3DFVF_XYZ|D3DFVF_DIFFUSE)
class CMyGame
{
private:
	struct CUSTOMVERTEX
	{
		FLOAT x, y, z;      // The untransformed, 3D position for the vertex
		DWORD color;        // The vertex color
	};
	LPDIRECT3DDEVICE9  m_pd3dDevice;
	LPDIRECT3DVERTEXBUFFER9 g_pVB;
public:
	CMyGame(LPDIRECT3DDEVICE9  pd3dDevice);
	void Render(float timeDre);
public:
	~CMyGame(void);
};
