#ifndef __WRAPPER__
#define __WRAPPER__

#pragma once

#include "../DLL/GenerateurCarte.h"
//#include "../DLL/SuggestionCase.h"
#include "../DLL/Point.h"
#include "../DLL/Enum.h"

using namespace System;

namespace Wrapper {

	public ref class Wrapper {

	public:
		static array<array<int>^>^ genererCarte(int size);
		static array<array<int>^>^ placementJoueur(int size);
		//static array<array<int>^>^ getSuggestion(array<array<int>^>^ carte, int taille, int peupleJoueur1, int peupleJoueur2, int x, int y, array<array<int>^>^ unites, int joueur);
	};
}

#endif