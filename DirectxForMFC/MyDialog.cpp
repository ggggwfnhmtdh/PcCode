// MyDialog.cpp : 实现文件
//

#include "stdafx.h"
#include "DirectxForMFC.h"
#include "MyDialog.h"


// MyDialog 对话框

IMPLEMENT_DYNAMIC(MyDialog, CDialog)

MyDialog::MyDialog(CWnd* pParent /*=NULL*/)
	: CDialog(MyDialog::IDD, pParent)
{

}

MyDialog::~MyDialog()
{
}

void MyDialog::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(MyDialog, CDialog)
	ON_BN_CLICKED(IDOK, &MyDialog::OnBnClickedOk)
END_MESSAGE_MAP()


// MyDialog 消息处理程序


void MyDialog::OnBnClickedOk()
{
	// TODO: 在此添加控件通知处理程序代码
	CDialog::OnOK();
}
