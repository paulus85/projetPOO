#include "SuggestionCase.h"


SuggestionCase::SuggestionCase(Case** carte, int taille, Peuple peuplej1, Peuple peuplej2) {
	this->carte = carte;
	this->taille = taille;
	this->peuples = (Peuple*)malloc(sizeof(Peuple) * 2);
	this->peuples[0] = peuplej1;
	this->peuples[1] = peuplej2;
}


Point* SuggestionCase::getSuggestion(int x, int y, Joueur** unites, Joueur j) const {
	Peuple peuple = peuples[j - 1];
	std::map<Point, int> scores;
	Point pos = Point(x, y);

	int xOffset[6] = { -1, -1, 0, 0, 1, 1 };
	int yOffset[6] = { 0, 1, -1, 1, 0, 1 };
	/*
	switch (peuple){
	case NAIN:
		for (int i = 0; i < 6; i++) {
			Point voisin = Point(xOffset[i] + x, yOffset[i] + y);
			int score = this->getScoreMouvement(voisin, peuple);
			score += this->getScoreCapture(voisin, unites[voisin.x][voisin.y], j, peuple);
			scores.insert(make_pair(voisin, score));
		}

		if (pos.estMontagne(this->carte)){
			//On prend toutes les cases montagnes et on les inclus dans le calcul
			for (int x = 0; x < this->taille; x++){
				for (int y = 0; y < this->taille; y++){
					Point voisin = Point(x, y);
					int score = this->getScoreMouvement(voisin, peuple);
					score += this->getScoreCapture(voisin, unites[voisin.x][voisin.y], j, peuple);
					scores.insert(make_pair(voisin, score));
				}
			}
		}

	default:
		for (int i = 0; i < 6; i++) {
			Point voisin = Point(xOffset[i] + pos.x, yOffset[i] + pos.y);
			int score = this->getScoreMouvement(voisin, peuple);
			score += this->getScoreCapture(voisin, unites[voisin.x][voisin.y], j, peuple);
			scores.insert(make_pair(voisin, score));
		}
	}
	*/
	Point* res = new Point[3];
	/*int nbResults = 0;
	for (int score = 3; nbResults < 3 && score > 0; score--) {
		for (std::map<Point, int>::iterator it = scores.begin(); nbResults<3 && it != scores.end(); ++it) {
			if (it->second == score) {
				res[nbResults] = it->first;
				nbResults++;
			}
		}
	}*/
	return res;
}


int SuggestionCase::getScoreMouvement(Point dest, Peuple peuple) const {
	Case square = this->carte[dest.x][dest.y];
	int scoreElf[4] = { 1, 0, 1, 1 };
	int scoreNain[4] = { 0, 1, 1, 1 };
	int scoresOrc[4] = { 1, 1, 1, 0 };
	int score = 0;
	switch (peuple){
	case ELF:
		score = scoreElf[square - 1];
	case NAIN:
		score = scoreNain[square - 1];
		if (dest.estMontagne(this->carte)) {
			// Le nain aura un avantage en pouvant se teleporter
			score++;
		}
	case ORC:
		score = scoresOrc[square - 1];
	}
	return score;
}


int SuggestionCase::getScoreCapture(Point dest, Joueur occupant, Joueur joueur, Peuple peuple) const {
	if (occupant == joueur) {
		return INT_MIN;
	}

	switch (peuple){
	case ORC:
		//Avec le point bonus l'orc devrait attaquer
		if (occupant != NONE)
			return 1;
		return 0;

	default:
		return 0;
	}
}
