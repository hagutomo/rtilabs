// RListView.cpp: RListView クラスのインプリメンテーション
//
//////////////////////////////////////////////////////////////////////

#include "comm.h"
#include "RListView.h"
//////////////////////////////////////////////////////////////////////
// 構築/消滅
//////////////////////////////////////////////////////////////////////

RListView::RListView()
{
}

RListView::~RListView()
{
}

void RListView::Create(HINSTANCE inINstance,
					   HWND isParentWindow,int inID, DWORD inFlg) throw()
{
	RWindow::Create(inINstance,WC_LISTVIEW,isParentWindow,inID,inFlg,0);
}

//拡張項目のセット.
void RListView::setExtended(DWORD inStyle)
{
	ListView_SetExtendedListViewStyle(this->getWindowHandle(), inStyle); 
}

//拡張項目のセット.
DWORD RListView::getExtended()
{
	return ListView_GetExtendedListViewStyle(this->getWindowHandle() );
}

//カラムをすべて削除する.
void RListView::ClearColumn()
{
	//すべのカラムの破棄
	while( ListView_DeleteColumn(this->getWindowHandle() ,0) );
}

//カラムアイテムの追加.
bool RListView::InsertColumn(int inItem,int inSubItem,char* inString,int inWidth,UINT inMask,int inFmt)
{
	LV_COLUMN lvcol = {0};
	lvcol.mask = inMask;
	lvcol.fmt = inFmt;
	lvcol.cx = inWidth;
	lvcol.pszText = inString;
	lvcol.iSubItem = inSubItem;
	return Btob(ListView_InsertColumn(this->getWindowHandle() , inItem, &lvcol));
}

//アイテムの追加.
bool RListView::SetItem(int inItem,int inSubItem,const char* inString)
{
	LVITEM	 lvitem = {0};
	lvitem.mask = LVIF_TEXT ;
	lvitem.pszText = (char*)inString;
	lvitem.iItem = inItem;
	lvitem.iSubItem = inSubItem;
	if (inSubItem == 0)
		return Btob(ListView_InsertItem(this->getWindowHandle() , &lvitem));
	else
		return Btob(ListView_SetItem(this->getWindowHandle() , &lvitem));
}

//アイテムの追加.
bool RListView::SetItem(int inItem,int inSubItem,int inInt)
{
	char buf[20];
	return SetItem(inItem,inSubItem, itoa( inInt , buf , 10) );
}

//アイテムの取得
string RListView::GetItem(int inItem,int inSubitem)
{
	char buf[MAX_PATH];

	LVITEM	 lvitem = {0};
	lvitem.mask = LVIF_TEXT ;
	lvitem.pszText = buf;
	lvitem.cchTextMax = MAX_PATH;
	lvitem.iItem = inItem;
	lvitem.iSubItem = inSubitem;
	bool ret = Btob( ListView_GetItem(this->getWindowHandle() , &lvitem) );
	if (!ret) return "";

	return lvitem.pszText;
}



//仮想リストの場合の項目数の設定
void  RListView::SetItemCountEx(int inCount,DWORD inFlg)
{
	ListView_SetItemCountEx(this->getWindowHandle() , inCount, inFlg);
}
//仮想リストの場合の項目数の設定
void  RListView::SetItemCount(int inCount) 
{
	ListView_SetItemCount(this->getWindowHandle() , inCount);
}

//現在選択されているところの取得.
int	RListView::getNowSelected() const
{
	return ListView_GetNextItem(this->getWindowHandle() ,-1,LVNI_SELECTED);
}




//アイテムの数の取得
int RListView::GetItemCount() const
{
	return ListView_GetItemCount(this->getWindowHandle() );
}

//inSearchIntがある位置をサーチ.
int RListView::SearchItem(int inSubItem , int inSearchInt)
{
	char buf[20];
	itoa( inSearchInt , buf , 10);

	int len = GetItemCount();

	for(int i  = 0; i < len ; i++)
	{
		if ( strcmp(GetItem( i ,inSubItem ).c_str() , buf) == 0 )return i;
	}
	return -1;
}

//アイテムの削除
bool RListView::DeleteItem(int inItem)
{
	ASSERT(inItem != -1);
	return Btob( ListView_DeleteItem(this->getWindowHandle() , inItem) );
}



//イメージリストセット.
bool RListView::setImageList(const HIMAGELIST inImageLIst , int inType)
{	
	return ListView_SetImageList(this->getWindowHandle() ,(HIMAGELIST)inImageLIst , inType) != NULL;
}



//指定したアイテムの状態を変更します。
void RListView::SetItemState(int inItem , int inState,int inMask)
{
	ASSERT(inItem != -1);

	ListView_SetItemState(this->getWindowHandle() ,inItem , inState,inMask);
}

//アイテムとのヒットテスト
int RListView::HitTest(const POINT inPoint) const
{
	LVHITTESTINFO hit = {0};
	hit.pt = inPoint;

	return ListView_HitTest(this->getWindowHandle() , &hit);
}


//指定したアイテムの状態を取得します。
int RListView::GetItemState(int inItem , int inMask) const
{
	ASSERT(inItem != -1);
	return ListView_GetItemState(this->getWindowHandle() ,inItem, inMask);
}

//すべての選択を解除.
void RListView::ResetSelect()
{
	int index = -1;
	while((index = ListView_GetNextItem(this->getWindowHandle(), index, LVNI_ALL | LVNI_SELECTED))!=-1)
	{
		SetItemState(index , 0 , LVIS_SELECTED | LVIS_FOCUSED);
	}
}

//現在マウスカーソルがのっかっているアイテムを取得します。
int RListView::HitItem() const
{
	//どこも選択されていない場合は、キャンセルする.
	POINT pt;
	GetCursorPos(&pt);

	//クライアント座標に持ってきて..
	ScreenToClient(this->getWindowHandle() , &pt);

	//参考 http://www.alpha-net.ne.jp/users2/uk413/vc/VCT_LVitem.html
	return this->HitTest(pt) ;
}
