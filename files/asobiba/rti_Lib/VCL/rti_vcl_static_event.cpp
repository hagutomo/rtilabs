/**********************************************************/
// ���X�g�{�b�N�X�̃C�x���g�Ƃ�
/**********************************************************/
#include "StartWithExeFile.h"
#ifdef STATICTEXT_LIFE

#include <.\VCL\rti_vcl_static_event.h>

/**********************************************************/
//�R���X�g���N�^   ���������܂�...
/**********************************************************/
TStaticEvent::TStaticEvent()
{
}
/**********************************************************/
//WM_COMMAND �C�x���g�f�B�X�p�b�`��
/**********************************************************/
void TStaticEvent::WmCommandCome(WPARAM wParam)
{
	switch(HIWORD(wParam))
	{
	}
}

#endif