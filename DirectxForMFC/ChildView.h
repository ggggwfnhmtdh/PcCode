// ChildView.h : CChildView 类的接口
//


#pragma once

#include "MyDialog.h"
#include "MyGame.h"
// CChildView 窗口

class CChildView : public CWnd
{
private:
	LPDIRECT3D9  m_pD3D; // Used to create the D3DDevice
	LPDIRECT3DDEVICE9  m_pd3dDevice; // Our rendering device

	MyDialog m_dlg;
	CMyGame *m_game;
// 构造
public:
	CChildView();
	HRESULT InitD3D( HWND hWnd ,int Width,int Height);
	float GetElaspedTime();
	void Update();
	void Render();
// 属性
public:

// 操作
public:

// 重写
	protected:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);

// 实现
public:
	virtual ~CChildView();

	// 生成的消息映射函数
protected:
	afx_msg void OnPaint();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
public:
	afx_msg void OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags);
};

