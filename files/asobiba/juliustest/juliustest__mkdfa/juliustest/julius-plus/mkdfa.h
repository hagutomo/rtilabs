#pragma once
#include <string>

//mkdfa.pl をC++に移植した
class mkdfa
{
public:
	bool conv(const std::string& gramprefix);
};
