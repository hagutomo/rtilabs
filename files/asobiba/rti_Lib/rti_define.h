#ifndef __RTI_DEFINE_H
#define __RTI_DEFINE_H

#include <.\Base\rti_glookup.h>

typedef struct BMP_DATA_TABLE* BMD;
struct BMP_DATA_TABLE{
	int w,h;
	BYTE *bm ;
	int  Pow2W;				// W が 2 のべき数だったら、ここに その数が示される.
    void* Tag;
};

//3次元
struct XYZ
{
    short x,y,z;
};

//3次元固定小数
struct XYZFixed
{
    Fixed x,y,z;
};

//3次元回転角度
struct XYZChar
{
    unsigned char x,y,z;
};

//2次元
struct XY
{
	short x,y;
};
//2次元固定
struct XYFixed
{
	Fixed x,y;
};

//RGB
struct _RGB
{
	unsigned char b,g,r;
};

typedef unsigned short Angle;			//回転角度(1024度).
typedef unsigned short Angle1024;		//回転角度(1024度).
#define __ANGLE_REV(x) (1023-(__ANGLE(x)))	//逆にする.
#define __ANGLE(x) ((x)&0x03ff)				//角度補正.

#ifdef _DEBUG
//ASSERT のパクリ
	#define __RTI_CHECKER(f) \
		do \
		{ \
			if (!(f) ) \
			{ \
				MSG msg;	\
				BOOL bQuit = PeekMessage(&msg, NULL, WM_QUIT, WM_QUIT, PM_REMOVE);	\
				if (bQuit)	PostQuitMessage(msg.wParam);	\
				__asm { int 3 }; \
			} \
		} while (0) 
#else
	#define __RTI_CHECKER(f) /*     */
#endif

#endif
