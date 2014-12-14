#include "Point.h"
#include "Enum.h"

Point::Point(int x, int y) {
	this->x = x;
	this->y = y;
}

Point::Point() {
	this->x = -1;
	this->y = -1;
}

bool Point::operator==(const Point& pt) const {
	return this->x == pt.x && this->y == pt.y;
}

bool Point::operator<(const Point& pt) const {
	return this->x > pt.x || (this->x == pt.x && this->y>pt.y);
}

bool Point::estValide(int size) const {
	if (this->x<0 || this->x >= size) {
		return false;
	}
	if (this->y<0 || this->y >= size) {
		return false;
	}
	return true;
}

bool Point::estPlaine(Case** map) const {
	return map[this->x][this->y] == PLAINE;
}