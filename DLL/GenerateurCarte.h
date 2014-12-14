#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

#include "Enum.h"
#include "Point.h"
#include <stdlib.h>
#include <time.h>

class DLL GenerateurCarte {

public:
	EXTERNC DLL static Case** genererCarte(int nbCases);
	EXTERNC DLL static Point* departUnites(int nbCases);
};


