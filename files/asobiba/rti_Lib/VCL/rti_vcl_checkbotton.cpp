/**********************************************************/
// �`�F�b�N�{�^��
/**********************************************************/
#include "StartWithExeFile.h"
#ifdef CHECKBUTTON_LIFE

#include <windows.h>
#include <.\VCL\rti_vcl_checkbotton.h>

TCheckButton::TCheckButton(TObject* hOwner,
	char *Caption,DWORD dwStyle,int x,int y,int w,int h,WNDPROC UserWndp)
{
    lstrcpy(ClassName,"BUTTON");
	//�{�^�������
    if (!CreateObject(hOwner,Caption,dwStyle | BS_AUTOCHECKBOX,x,y,w,h,false,UserWndp) )
    {   //�쐬���s
        Kind = OBJECT_KIND_BAD;
        return ;
    }
    //�����̃I�[�i�[�Ɏ������쐬���ꂽ���Ƃ�`����
    OwnerAdd(hOwner,this);

    Kind            = OBJECT_KIND_CHECKBUTTON;

}

TCheckButton::~TCheckButton()
{
	DestroyComponent();
} 

/**********************************************************/
/* ��{���[�`�� */
/**********************************************************/
LRESULT TCheckButton::UserDisPacth(HWND hwnd , UINT message,
					 WPARAM wParam,LPARAM lParam)
{
    switch(message)
    {
    }
    return TBaseObject::UserDisPacth(hwnd,message,wParam,lParam);
}

#endif //LIFE END