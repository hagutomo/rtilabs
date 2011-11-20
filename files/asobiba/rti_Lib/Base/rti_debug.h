#include <StartWithExeFile.h>
#ifdef DEBUG_LIFE

#ifndef ___DEBUGH
#define ___DEBUGH

void DEBUG(char *str);
void DEBUG2(int i);
void DEBUG3(char* Format,...);

//時間計測を行う場合.
#ifdef TIME_CHECK

void NewRecordFPS(unsigned long rec);
void WriteRecordFPS(unsigned long DesignatedTime);
#define NOW_TIME(rec) NewRecordFPS(rec)
#define END_TIME(rec) WriteRecordFPS(rec)

#else

#define NOW_TIME(rec) /* */
#define END_TIME(rec) /* */

#endif	//時間計測 END



#endif	//DEBUG END

#endif //LIEF END
