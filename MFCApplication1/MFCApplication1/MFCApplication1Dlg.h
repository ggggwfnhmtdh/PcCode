
// MFCApplication1Dlg.h : header file
//

#pragma once
#include "afxcmn.h"


// CMFCApplication1Dlg dialog
class CMFCApplication1Dlg : public CDialogEx
{
// Construction
public:
	CMFCApplication1Dlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_MFCAPPLICATION1_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	void ArrayShowPic(int x, int y, unsigned char*data);
	afx_msg void OnBnClickedButton1();
	afx_msg void OnBnClickedButton2();
	void ShowPress(int Value, int Max);
	void ShowCirCle(int x, int y, int R);
	CTreeCtrl m_tree;
	afx_msg void OnBnClickedCancel();
};
