#ifndef __WRAPPER__
#define __WRAPPER__

#include "../DLL/Algo.h" // A changer
// Le bon chemin pour être "../Debug/nomProjetLib.lib"
#pragma comment(lib, "../Debug/DLL.lib") // A changer

using namespace System;

namespace Wrapper {
	public ref class WrapperAlgo {
	private:
		Algo* algo;
		public:
		WrapperAlgo(){ algo = Algo_new(); }
		~WrapperAlgo(){ Algo_delete(algo); }
		int computeFoo() { return algo->computeFoo(); }
	};
}
#endif