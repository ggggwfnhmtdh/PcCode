#include<stdio.h>
unsigned int SumData = 0;
unsigned char CountIndex = 0;
unsigned char OverFlag = 0;
#define P_MAX 3

unsigned int  AverageFilter(unsigned int InputY);

main()
{
     unsigned int  i = 0;
     unsigned int j = 0;
	 for(i=0;i<=10;i++)
	 {
	     j =  AverageFilter(i);   
	     printf("%d-----%d\n",i,j);
 	 }   
}
/******************ƽ���˲��㷨*************************
Input  : �˲�����Դ 
OutPut ���˲������� 
��������
P_MAX:�˲����ڿ�� 
unsigned int SumData = 0;
unsigned char CountIndex = 0;
unsigned char OverFlag = 0;
*******************************************************/ 
 
unsigned int  AverageFilter(unsigned int InputData)
{
	static unsigned int DataArray[P_MAX];
	int LastData = 0;
	unsigned int ReplaceData = 0;
	static unsigned int CurrentData = 0;
	unsigned int OutPutData = 0;
	LastData = CurrentData;
	if( CountIndex >= P_MAX )
    {
	    CountIndex = 0;
		ReplaceData = DataArray[CountIndex];
		DataArray[CountIndex] = InputData;
		CurrentData = DataArray[CountIndex];
		CountIndex ++;	
		OverFlag = 1;    //���������־λ
 	}
	else
	{
		ReplaceData = DataArray[CountIndex];
		DataArray[CountIndex] = InputData;
		CurrentData = DataArray[CountIndex];
		CountIndex++;
	}
	
	if(OverFlag)
	{
		SumData = SumData + CurrentData - ReplaceData;
		OutPutData = SumData/P_MAX;
	}  
	else
	{ 
		SumData = SumData + DataArray[CountIndex-1];
		OutPutData = SumData/CountIndex;
	}
	return OutPutData;
}
