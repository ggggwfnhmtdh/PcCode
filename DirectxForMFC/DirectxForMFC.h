// DirectxForMFC.h : DirectxForMFC Ӧ�ó������ͷ�ļ�
//
#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#include "resource.h"       // ������


// CDirectxForMFCApp:
// �йش����ʵ�֣������ DirectxForMFC.cpp
//

class CDirectxForMFCApp : public CWinApp
{
public:
	CDirectxForMFCApp();


// ��д
public:
	virtual BOOL InitInstance();

// ʵ��

public:
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnIdle(LONG lCount);
public:
	virtual int Run();
};

extern CDirectxForMFCApp theApp;