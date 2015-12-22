// ChildView.h : CChildView ��Ľӿ�
//


#pragma once

#include "MyDialog.h"
#include "MyGame.h"
// CChildView ����

class CChildView : public CWnd
{
private:
	LPDIRECT3D9  m_pD3D; // Used to create the D3DDevice
	LPDIRECT3DDEVICE9  m_pd3dDevice; // Our rendering device

	MyDialog m_dlg;
	CMyGame *m_game;
// ����
public:
	CChildView();
	HRESULT InitD3D( HWND hWnd ,int Width,int Height);
	float GetElaspedTime();
	void Update();
	void Render();
// ����
public:

// ����
public:

// ��д
	protected:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);

// ʵ��
public:
	virtual ~CChildView();

	// ���ɵ���Ϣӳ�亯��
protected:
	afx_msg void OnPaint();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
public:
	afx_msg void OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags);
};

