// RGISNameDatabaseDisk.cpp: RGISNameDatabaseDisk クラスのインプリメンテーション
//
//////////////////////////////////////////////////////////////////////

#include "RGISNameDatabaseDisk.h"
#include "RStdioFile.h"
#include "RDiskUtil.h"


//////////////////////////////////////////////////////////////////////
// 構築/消滅
//////////////////////////////////////////////////////////////////////

RGISNameDatabaseDisk::RGISNameDatabaseDisk()
{

}

RGISNameDatabaseDisk::~RGISNameDatabaseDisk()
{

}

unsigned short RGISNameDatabaseDisk::Write(const string & inFilename , const string & inName) throw (RException)
{
	RStdioFile file;

	//文字列は、 \0 区切りで登録されています。
	file.Open(inFilename , "a+b");

	long size = file.getSize();
	char* buffer = new char[size + 1];

	int line = 0;
	if (size > 0)
	{
		file.fseek(0);	//先頭に.
		file.fread(buffer , size );	//全部読み...

		//で、 inName って登録されていまっする?
		int addpoint = 0;
		for(line = 0 ;  ; line++)
		{
			const char * name = buffer + addpoint ;

			if ( strcmp(inName.c_str() , name) == 0)
			{
				delete [] buffer ;
				//同じ文字列があったのでそいつを共有するのです。
				return line;
			}
			//次の文字へ.
			addpoint += strlen(name) + 1;	//+1は \0の分.
			if (addpoint >= size)
			{
				line ++;
				break;	//これ以上ないのです。
			}
		}
	}
	//うーん、登録されていないようですね。
	//では、新規登録ということで...

	file.fseek(0,SEEK_END);	//最後部に.
	file.fwrite(inName.c_str() , inName.size() + 1);

	delete [] buffer ;

	if (line > 0xffff)
	{
		throw RException(EXCEPTIONTRACE + "文字列が 65535 を超えました");
	}
	return line ;
}

void RGISNameDatabaseDisk::test()
{
	string file = "test/namedatabasedisk.test";
	
	try
	{
		RDiskUtil::Delete(file);
	}
	catch(RException e)
	{
	}

	RGISNameDatabaseDisk disk;
	int r;
	r = disk.Write(file , "あにちゃま");
	ASSERT(r == 0);
	r = disk.Write(file , "あにちゃま");
	ASSERT(r == 0);
	r = disk.Write(file , "あにぎみさま");
	ASSERT(r == 1);
	r = disk.Write(file , "あにぎみさま");
	ASSERT(r == 1);
	r = disk.Write(file , "おにぃちゃま");
	ASSERT(r == 2);
	r = disk.Write(file , "あにちゃま");
	ASSERT(r == 0);
}
