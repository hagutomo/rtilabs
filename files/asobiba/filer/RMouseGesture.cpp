// RMouseGesture.cpp: RMouseGesture �N���X�̃C���v�������e�[�V����
//
//////////////////////////////////////////////////////////////////////

#include "comm.h"
#include "RMouseGesture.h"

//////////////////////////////////////////////////////////////////////
// �\�z/����
//////////////////////////////////////////////////////////////////////

RMouseGesture::RMouseGesture()
{

}

RMouseGesture::~RMouseGesture()
{

}

RMouseGesture* RMouseGesture::getInstance()
{
	static RMouseGesture	rmg;
	return &rmg;
}
