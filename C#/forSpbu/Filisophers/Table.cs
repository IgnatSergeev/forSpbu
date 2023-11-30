
class Table {
    public Table(int gameNumber) {
        gameNumber = gameNumber;
        currentForkNumber = gameNumber;
    }

    public bool GiveFork() {

        if (currentForkNumber > 0) {
            currentForkNumber -= 1;
            return true;
        }
        return false;
    }

    public void GetFork() {
        currentForkNumber++;
    }

    int currentForkNumber = 0;
    int gameNumber = 0;
}