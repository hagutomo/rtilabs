/*
#include <.\Base\rti_glookup.h>

//方向をあらわす.
enum Houkou
{
	TOP,	RIGHT,	BUTTON,	LEFT
};

class TBasicDefines;

class TBasicDefines
{
public:
	TBasicDefines(){X = 0; Y = 0; Sx = 0 ; Sy = 0; Angle = 0; Speed = 0;};

	Fixed X,Y;	//X Y データ
	Fixed Sx,Sy;//速度
	unsigned char Angle;	//回転角
	Fixed Speed;			//速度

	int GetX(){return FI(X);};
	int GetY(){return FI(Y);};
	int GetSpeed(){return FI(Speed);};
	unsigned char GetAngle(){return Angle;};
	void SetX(int x){X = IF(x);};
	void SetY(int y){Y = IF(y);};
	void SetSpeed(int speed){Speed = IF(speed);};
	void SetAngle(unsigned char angle){Angle = angle;};

	//敵機を追っかける.
	void PathFinder(TBasicDefines *TBO)
	{
		unsigned char Seeta;
	    Fixed x =  TBO->X - X;
		Fixed y =  TBO->Y - Y;
		Seeta = (unsigned char) DDD (atan2( FI(y) , FI(x) ) );
		Angle = Seeta;
	    Sx = FixedMul(FixedLTBCos256[ Seeta ] , Speed) ;
	    Sy = FixedMul(FixedLTBSin256[ Seeta ] , Speed) ;
	};
	//ホーミング
	void Homing(TBasicDefines *TBO)
	{
		//自分と目標との座標の差から目標角を求める
	    Fixed kdx =  TBO->X - X;
		Fixed kdy =  TBO->Y - Y;
		double rad = atan2( FI(kdy) , FI(kdx) );

		//差角を求める(目標角 - 進行角)
		double subdir = rad - RRR(Angle);	//360座標系

		//遠回りしないための処理(180 以内でなければ、逆角)
		if (subdir > RRR(180) )		subdir -= RRR(360);
		if (subdir < RRR(-180) )	subdir += RRR(360);

		//目標方向へ、少し向かせる.(進行角 += 差角 * 割合)
		double dir = subdir * 0.05;	//差に対しての割合.
		Angle += (unsigned char)DDD256(dir);

		//進行角から移動量を求め足し込む
		Sx = FixedMul(FixedLTBCos256[Angle] , Speed);
		Sy = FixedMul(FixedLTBSin256[Angle] , Speed);
	};
	void MoveRef()
	{
		X += Sx;
		Y += Sy;
	};
};

*/
