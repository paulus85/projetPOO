/**
* \file      SuggestionCase.ccp
* \author    Poilane Pierre
* \version   1.0
* \date      08/01/2015
* \brief     Suggère des cases où le joueur peut se déplacer
*
* \details   Suggère au maximum 3 cases, ainsi les cases suggérées ne sont pas toutes les cases où le joueur peut se déplacer
*/

#include "SuggestionCase.h"
#include "Point.h"

/**
* \brief       Constructeur de la classe
* \details	   initialise la carte des cases du jeu, sa taille et le peuple des 2 joueurs
* \param       carte			La carte des cases du jeu en cours
* \param       taille			La taille de la carte
* \param       PeupleJoueur1    Le peuple du joueur 1
* \param       PeupleJoueur2    Le peuple du joueur 2
*/
SuggestionCase::SuggestionCase(Case** carte, int taille, Peuple peuplej1, Peuple peuplej2) {
	this->carte = carte;
	this->taille = taille;
	this->peuples = (Peuple*)malloc(sizeof(Peuple) * 2);
	this->peuples[0] = peuplej1;
	this->peuples[1] = peuplej2;
}

/**
* \brief       Retourne des cases suggérées
* \details	   Les cases suggérées tiennent compte des points que rapportent la case visée, à l'unité se trouvant sur la case visée
*				ainsi que la petinence de déplacement et des points de déplacement de l'unité sélectionnée
* \param       x				L'abscisse de la case courante
* \param       y				L'ordonnée de la case courante
* \param       unites			La matrice des unités présentes sur la carte
* \param       ptsDeplacement   Les points de déplacement de l'unité sélectionnée
* \param       joueur			Le joueur courant
* \return      Les points suggérés
*/
Point* SuggestionCase::getSuggestion(int x, int y, Joueur** unites, double** ptsDeplacement, Joueur joueur) const {
	Peuple peuple = peuples[joueur - 1];
	map<Point, int> scores;
	Point pos = Point(x, y);

	int xOffset[6];
	int yOffset[6];

	this->getVoisin(x, xOffset, yOffset);

	switch (peuple){
		case NAIN:
			for (int i = 0; i < 6; i++) {
				Point voisin = Point(xOffset[i] + x, yOffset[i] + y);
				if(voisin.estValide(taille))
				{
					int score = this->getScoreMouvement(voisin, peuple);
					score += this->getScoreCapture(unites[voisin.x][voisin.y], joueur, peuple);
					score += this->getScoreDeplacement(voisin, ptsDeplacement[x][y], peuple);
					score += this->getScoreBonusAttaqueDefense(voisin, peuple);
					scores.insert(make_pair(voisin, score));
				}
			}

			if (pos.estMontagne(this->carte)){
				//On prend toutes les cases montagnes et on les inclus dans le calcul
				for (int i = 0; i < this->taille; i++){
					for (int j = 0; j < this->taille; j++){
						Point voisin = Point(i, j);
						if (voisin.estValide(taille) && voisin.estMontagne(this->carte) && voisin.x != x && voisin.y != y && unites[voisin.x][voisin.y] != (joueur || NONE)){
							int score = this->getScoreMouvement(voisin, peuple);
							score += this->getScoreCapture(unites[voisin.x][voisin.y], joueur, peuple);
							score += this->getScoreDeplacement(voisin, ptsDeplacement[x][y], peuple);
							score += this->getScoreBonusAttaqueDefense(voisin, peuple);
							scores.insert(make_pair(voisin, score));
						}
					}
				}
			}
			break;

		default:
			for (int i = 0; i < 6; i++) {
				Point voisin = Point(xOffset[i] + pos.x, yOffset[i] + pos.y);
				if (voisin.estValide(taille))
				{
					int score = this->getScoreMouvement(voisin, peuple);
					score += this->getScoreCapture(unites[voisin.x][voisin.y], joueur, peuple);
					score += this->getScoreDeplacement(voisin, ptsDeplacement[x][y], peuple);
					score += this->getScoreBonusAttaqueDefense(voisin, peuple);
					scores.insert(make_pair(voisin, score));
				}
			}
			break;
	}
	
	Point* res = new Point[3];
	int nbResults = 0;
	for (int score = 5; nbResults < 3 && score > 0; score--) {
		for (map<Point, int>::iterator it = scores.begin(); nbResults<3 && it != scores.end(); it++) {
			if (it->second > 0 && it->second == score) {
				res[nbResults] = it->first;
				nbResults++;
			}
		}
	}

	return res;
}

/**
* \brief       Donne les coodonnées des cases correspondant aux cases voisines de la case courante
* \param       x           la coordonées x permettant de savoir quelle coordonnées il faut renvoyées
* \param       *xOffset    le pointeur pou les coodonnées x voisines
* \param       *yOffset    le pointeur pou les coodonnées y voisines
*/
void SuggestionCase::getVoisin(int x, int xOffset[6], int yOffset[6]) const{
	if (x % 2 == 0){
		int tabX[6] = { -1, -1, 0, 0, 1, 1 };
		int tabY[6] = { -1, 0, -1, 1, -1, 0 };

		for (int i = 0; i < 6; i++){
			xOffset[i] = tabX[i];
			yOffset[i] = tabY[i];
		}
	}
	else if (x % 2 == 1){
		int tabX[6] = { -1, -1, 0, 0, 1, 1 };
		int tabY[6] = { 0, 1, -1, 1, 0, 1 };

		for (int i = 0; i < 6; i++){
			xOffset[i] = tabX[i];
			yOffset[i] = tabY[i];
		}
	}
}

/**
* \brief       Retourne le score par rapport au point que rapporte une case en fonction du peuple du joueur courant
* \param       pos       Le point destination visé
* \param       peuple    Le peuple du joueur courant
*/
int SuggestionCase::getScoreMouvement(Point dest, Peuple peuple) const {
	Case square = this->carte[dest.x][dest.y];
	int scoresElf[5] = { 1, 0, 1, 1, 1 };
	int scoresNain[5] = { 0, 1, 1, 1, 1};
	int scoresOrc[5] = { 1, 1, 1, 0, 1};
	int scoresZombie[5] = { 1, 0, 1, 1, 1 };
	int score = 0;
	switch (peuple){
		case ELF:
			score = scoresElf[square]; break;
		case NAIN:
			score = scoresNain[square]; break;
		case ORC:
			score = scoresOrc[square]; break;
		case ZOMBIE:
			score = scoresZombie[square]; break;
		default:
			break;
	}
	return score;
}

/**
* \brief       Retourne le score par rapport à l'unité se trouvant sur la case destination
* \param       occupant     Le joueur se trouvant sur la case destination
* \param       joueur       Le joueur courant
* \param       peuple       Le peuple du joueur courant
*/
int SuggestionCase::getScoreCapture(Joueur occupant, Joueur joueur, Peuple peuple) const {
	if (occupant == joueur) {
		return -5;
	}

	switch (peuple){
		case ORC:
			//Avec le point bonus l'orc devrait attaquer
			if (occupant != NONE){
				return 2; break;
			}
			return 0; break;
		case ZOMBIE:
			//Avec sa capacité à attaquer 2 fois le zombie devrait attaquer
			if (occupant != NONE){
				return 2; break;
			}
			return 0; break;

		default:
			return 0; break;
	}
}

/**
* \brief       Retourne le score par rapport à la pertinence du déplacement et aux points de déplacement de l'unité
* \param       dest				Le point de destination
* \param       ptsDeplacement   Les points de déplacement de l'unité sélectionnée
* \param       peuple           Le peuple du joueur courant
*/
int SuggestionCase::getScoreDeplacement(Point dest, double ptsDeplacement, Peuple peuple) const {
	Case square = this->carte[dest.x][dest.y];
	int scoreElf[5] = { 0, -1, 0, 1, 0 }; double deplElf[5] = { 1, 2, 1, 0.5, 1 };
	int scoreNain[5] = { 1, 0, 1, 0, 0 }; double deplNain[5] = { 0.5, 1, 1, 1, 1 };
	int scoresOrc[5] = { 1, 0, 0, 0, 0 }; double deplOrc[5] = { 0.5, 1, 1, 1, 1 };
	int scoresZombie[5] = { 0, 0, 0, 0, 1 }; double deplZombie[5] = { 1, 1, 1, 1, 0.5 };
	int score = 0;
	switch (peuple){
		case ELF:
			score = scoreElf[square];
			if (ptsDeplacement < deplElf[square])
				score = INT_MIN;
			break;
		case NAIN:
			score = scoreNain[square];
			if (ptsDeplacement < deplNain[square])
				score = INT_MIN;
			break;
		case ORC:
			score = scoresOrc[square];
			if (ptsDeplacement < deplOrc[square])
				score = INT_MIN;
			break;
		case ZOMBIE:
			score = scoresZombie[square];
			if (ptsDeplacement < deplZombie[square])
				score = INT_MIN;
			break;
		default:
			break;
	}
	return score;
}

/**
* \brief       Retourne le score par rapport au bonus terrain qui varie l'attaque et la défense de l'unité
* \param       dest				Le point de destination
* \param       peuple           Le peuple du joueur courant
*/
int SuggestionCase::getScoreBonusAttaqueDefense(Point dest, Peuple peuple) const {
	Case square = this->carte[dest.x][dest.y];
	int scoreElf[5] = { 0, 0, 0, 1, -1 };
	int scoreNain[5] = { -1, 0, 1, 0, 0 };
	int scoresOrc[5] = { 1, 0, 0, -1, 0 };
	int scoresZombie[5] = { 0, 0, -1, 0, 1 };
	int score = 0;
	switch (peuple){
		case ELF:
			score = scoreElf[square];
			break;
		case NAIN:
			score = scoreNain[square];
			break;
		case ORC:
			score = scoresOrc[square];
			break;
		case ZOMBIE:
			score = scoresZombie[square];
			break;
		default:
			break;
	}
	return score;
}
