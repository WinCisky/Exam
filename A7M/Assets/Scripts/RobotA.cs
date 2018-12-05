using UnityEngine;
using System.Collections;
public class RobotA : MonoBehaviour
{
    //struct declaration
    public struct CellPos
    {
        public int x;
        public int y;
    };
    public struct Cell
    {
        public bool visited;
        public int x;
        public int y;
        public bool north;
        public bool east;
        public bool west;
        public bool south;
        public bool associate;
    };
    public struct Positions
    {
        public CellPos position;
        public string orientation;
    };
    public struct Way
    {
        public CellPos box1;
        public CellPos box2;
    };
    //variables declaration
    public GameObject Robot;
    public GameObject StartingPoint;
    public GameObject VisitedCell;
    public float updateTime;
    public int posx, posy, lengthJourneyBack;
    public string direction;
    public bool isSpawnSpowned = false;
    public bool isRobotStarted = false;
    public bool goToPos = false;
    public bool finished = false;
    int n_listPos = 0;
    int element;
    Positions[] listPos; //Tutte le caselle da visitare
    Positions SpawnPoint;
    CellPos posToReach;
    CellPos[] newPos;
    CellPos[] oldPos;
    CellPos[] journeyBack;
    Cell[,] robotMap;
    Way[] journey;
    int steps = 0;
    int turns = 0;

    //Controllo se ci sono i muri, 
    //aggiorna la mappa del robot, 
    //inserisce la posizione nella lista delle caselle da visitare
    void CheckPos(string dir)
    {
        if (dir == "forward")
        {
            if (direction == "north")
            {
                if (!GetComponent<Maze>().map[posx, posy].north)
                {
                    listPos[n_listPos].orientation = "north";
                    robotMap[posx, posy].north = false;
                    robotMap[posx, posy + 1].south = false;
                    listPos[n_listPos].position.x = posx;
                    listPos[n_listPos].position.y = posy;
                    n_listPos++;
                }
            }
            else if (direction == "south")
            {
                if (!GetComponent<Maze>().map[posx, posy].south)
                {
                    listPos[n_listPos].orientation = "south";
                    robotMap[posx, posy].south = false;
                    robotMap[posx, posy - 1].north = false;
                    listPos[n_listPos].position.x = posx;
                    listPos[n_listPos].position.y = posy;
                    n_listPos++;
                }
            }
            else if (direction == "east")
            {
                if (!GetComponent<Maze>().map[posx, posy].east)
                {
                    listPos[n_listPos].orientation = "east";
                    robotMap[posx, posy].east = false;
                    robotMap[posx + 1, posy].west = false;
                    listPos[n_listPos].position.x = posx;
                    listPos[n_listPos].position.y = posy;
                    n_listPos++;
                }
            }
            else if (direction == "west")
            {
                if (!GetComponent<Maze>().map[posx, posy].west)
                {
                    listPos[n_listPos].orientation = "west";
                    robotMap[posx, posy].west = false;
                    robotMap[posx - 1, posy].east = false;
                    listPos[n_listPos].position.x = posx;
                    listPos[n_listPos].position.y = posy;
                    n_listPos++;
                }
            }
        }
        else if (dir == "left")
        {
            if (direction == "north")
            {
                if (!GetComponent<Maze>().map[posx, posy].west)
                {
                    listPos[n_listPos].orientation = "west";
                    robotMap[posx, posy].west = false;
                    robotMap[posx - 1, posy].east = false;
                    listPos[n_listPos].position.x = posx;
                    listPos[n_listPos].position.y = posy;
                    n_listPos++;
                }
            }
            else if (direction == "south")
            {
                if (!GetComponent<Maze>().map[posx, posy].east)
                {
                    listPos[n_listPos].orientation = "east";
                    robotMap[posx, posy].east = false;
                    robotMap[posx + 1, posy].west = false;
                    listPos[n_listPos].position.x = posx;
                    listPos[n_listPos].position.y = posy;
                    n_listPos++;
                }
            }
            else if (direction == "east")
            {
                if (!GetComponent<Maze>().map[posx, posy].north)
                {
                    listPos[n_listPos].orientation = "north";
                    robotMap[posx, posy].north = false;
                    robotMap[posx, posy + 1].south = false;
                    listPos[n_listPos].position.x = posx;
                    listPos[n_listPos].position.y = posy;
                    n_listPos++;
                }
            }
            else if (direction == "west")
            {
                if (!GetComponent<Maze>().map[posx, posy].south)
                {
                    listPos[n_listPos].orientation = "south";
                    robotMap[posx, posy].south = false;
                    robotMap[posx, posy - 1].north = false;
                    listPos[n_listPos].position.x = posx;
                    listPos[n_listPos].position.y = posy;
                    n_listPos++;
                }
            }
        }
        else if (dir == "right")
        {
            if (direction == "north")
            {
                if (!GetComponent<Maze>().map[posx, posy].east)
                {
                    listPos[n_listPos].orientation = "east";
                    robotMap[posx, posy].east = false;
                    robotMap[posx + 1, posy].west = false;
                    listPos[n_listPos].position.x = posx;
                    listPos[n_listPos].position.y = posy;
                    n_listPos++;
                }
            }
            else if (direction == "south")
            {
                if (!GetComponent<Maze>().map[posx, posy].west)
                {
                    listPos[n_listPos].orientation = "west";
                    robotMap[posx, posy].west = false;
                    robotMap[posx - 1, posy].east = false;
                    listPos[n_listPos].position.x = posx;
                    listPos[n_listPos].position.y = posy;
                    n_listPos++;
                }
            }
            else if (direction == "east")
            {
                if (!GetComponent<Maze>().map[posx, posy].south)
                {
                    listPos[n_listPos].orientation = "south";
                    robotMap[posx, posy].south = false;
                    robotMap[posx, posy - 1].north = false;
                    listPos[n_listPos].position.x = posx;
                    listPos[n_listPos].position.y = posy;
                    n_listPos++;
                }
            }
            else if (direction == "west")
            {
                if (!GetComponent<Maze>().map[posx, posy].north)
                {
                    listPos[n_listPos].orientation = "north";
                    robotMap[posx, posy].north = false;
                    robotMap[posx, posy + 1].south = false;
                    listPos[n_listPos].position.x = posx;
                    listPos[n_listPos].position.y = posy;
                    n_listPos++;
                }
            }
        }
        else if (dir == "behind")
        {
            //inserita solo la prima volta so south is the orientation
            if (!GetComponent<Maze>().map[posx, posy].south)
            {
                listPos[n_listPos].orientation = "south";
                robotMap[posx, posy].south = false;
                robotMap[posx, posy - 1].north = false;
                listPos[n_listPos].position.x = posx;
                listPos[n_listPos].position.y = posy;
                n_listPos++;
            }
        }
    }
    void GoForward()
    {
        if (direction == "north")
        {
            posy++;
        }
        else if (direction == "east")
        {
            posx++;
        }
        else if (direction == "west")
        {
            posx--;
        }
        else if (direction == "south")
        {
            posy--;
        }

        robotMap[posx, posy].visited = true;

        Instantiate(VisitedCell, new Vector3(posx, posy), Quaternion.identity);

        CheckPos("left");
        CheckPos("forward");
        CheckPos("right");

        steps++;
    }
    void Advance()
    {
        if (direction == "north")
        {
            posy++;
        }
        else if (direction == "east")
        {
            posx++;
        }
        else if (direction == "west")
        {
            posx--;
        }
        else if (direction == "south")
        {
            posy--;
        }

        steps++;
    }
    bool AlreadyVisited(Positions spot)
    {
        bool isVisited = false;
        if (spot.orientation == "north")
        {
            if (robotMap[spot.position.x, spot.position.y + 1].visited)
            {
                isVisited = true;
            }
        }
        else if (spot.orientation == "south")
        {
            if (robotMap[spot.position.x, spot.position.y - 1].visited)
            {
                isVisited = true;
            }
        }
        else if (spot.orientation == "east")
        {
            if (robotMap[spot.position.x + 1, spot.position.y].visited)
            {
                isVisited = true;
            }
        }
        else if (spot.orientation == "west")
        {
            if (robotMap[spot.position.x - 1, spot.position.y].visited)
            {
                isVisited = true;
            }
        }
        if (isVisited)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void ResetMapAssociations()
    {
        for (int i = 0; i < (GetComponent<Maze>().lengthx + 1); i++)
        {
            for (int u = 0; u < (GetComponent<Maze>().lengthy + 1); u++)
            {
                robotMap[i, u].associate = false;
            }
        }
    }
    void Turn(string turningSide)
    {
        if (turningSide == "right")
        {
            if (direction == "north")
            {
                direction = "east";
                Robot.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            }
            else if (direction == "east")
            {
                direction = "south";
                Robot.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            }
            else if (direction == "south")
            {
                direction = "west";
                Robot.transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
            }
            else if (direction == "west")
            { // direction == ovest
                direction = "north";
                Robot.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            }
        }
        else if (turningSide == "left")
        {
            if (direction == "north")
            {
                direction = "west";
                Robot.transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
            }
            else if (direction == "east")
            {
                direction = "north";
                Robot.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            }
            else if (direction == "south")
            {
                direction = "east";
                Robot.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            }
            else if (direction == "west")
            { // direction == west
                direction = "south";
                Robot.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            }
        }
        else
        {
            if (direction == "north")
            {
                direction = "south";
                Robot.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            }
            else if (direction == "east")
            {
                direction = "west";
                Robot.transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
            }
            else if (direction == "south")
            {
                direction = "north";
                Robot.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            }
            else if (direction == "west")
            { // direction == ovest
                direction = "east";
                Robot.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            }
        }

        turns++;
    }
    void ComputeTurn(string directionToTurn)
    {
        if (directionToTurn == "north")
        {
            if (direction == "south")
            {
                Turn("behind");
            }
            else if (direction == "east")
            {
                Turn("left");
            }
            else if (direction == "west")
            {
                Turn("right");
            }
        }
        else if (directionToTurn == "south")
        {
            if (direction == "north")
            {
                Turn("behind");
            }
            else if (direction == "west")
            {
                Turn("left");
            }
            else if (direction == "east")
            {
                Turn("right");
            }
        }
        else if (directionToTurn == "east")
        {
            if (direction == "west")
            {
                Turn("behind");
            }
            else if (direction == "north")
            {
                Turn("right");
            }
            else if (direction == "south")
            {
                Turn("left");
            }
        }
        else if (directionToTurn == "west")
        {
            if (direction == "east")
            {
                Turn("behind");
            }
            else if (direction == "south")
            {
                Turn("right");
            }
            else if (direction == "north")
            {
                Turn("left");
            }
        }
    }
    void Wavefront(Positions goal)
    {
        if ((goal.position.x != posx) || (goal.position.y != posy))
        {
            //devo arrivare alla casella goal
            //inizializzo le variabili e le liste
            bool isGoalReached = false;
            int lengthListNewPos = 0;
            int lengthOldPos = 0;
            int lengthJourney = 0;
            int i = 0;
            ResetMapAssociations();
            oldPos[lengthOldPos].x = posx;
            oldPos[lengthOldPos].y = posy;
            robotMap[oldPos[i].x, oldPos[i].y].associate = true;
            lengthOldPos++;

            //trovo il percorso
            while (!isGoalReached)
            {
                i = 0;
                while ((i < lengthOldPos) && (!isGoalReached))
                {
                    //nord
                    if (!robotMap[oldPos[i].x, oldPos[i].y].north)
                    {
                        if (!robotMap[oldPos[i].x, oldPos[i].y + 1].associate)
                        {
                            robotMap[oldPos[i].x, oldPos[i].y + 1].associate = true;
                            newPos[lengthListNewPos].x = oldPos[i].x;
                            newPos[lengthListNewPos].y = (oldPos[i].y + 1);
                            //devo tenere traccia del percorso che ho fatto!
                            journey[lengthJourney].box1.x = oldPos[i].x;
                            journey[lengthJourney].box1.y = oldPos[i].y;
                            journey[lengthJourney].box2.x = newPos[lengthListNewPos].x;
                            journey[lengthJourney].box2.y = newPos[lengthListNewPos].y;
                            lengthListNewPos++;
                            lengthJourney++;
                            if ((oldPos[i].x == goal.position.x) && ((oldPos[i].y + 1) == goal.position.y))
                            {
                                isGoalReached = true;
                            }
                        }
                    }
                    //sud
                    if (!robotMap[oldPos[i].x, oldPos[i].y].south)
                    {
                        if (!robotMap[oldPos[i].x, oldPos[i].y - 1].associate)
                        {
                            robotMap[oldPos[i].x, oldPos[i].y - 1].associate = true;
                            newPos[lengthListNewPos].x = oldPos[i].x;
                            newPos[lengthListNewPos].y = (oldPos[i].y - 1);
                            //devo tenere traccia del percorso che ho fatto!
                            journey[lengthJourney].box1.x = oldPos[i].x;
                            journey[lengthJourney].box1.y = oldPos[i].y;
                            journey[lengthJourney].box2.x = newPos[lengthListNewPos].x;
                            journey[lengthJourney].box2.y = newPos[lengthListNewPos].y;
                            lengthListNewPos++;
                            lengthJourney++;
                            if ((oldPos[i].x == goal.position.x) && ((oldPos[i].y - 1) == goal.position.y))
                            {
                                isGoalReached = true;
                            }
                        }
                    }
                    //est
                    if (!robotMap[oldPos[i].x, oldPos[i].y].east)
                    {
                        if (!robotMap[oldPos[i].x + 1, oldPos[i].y].associate)
                        {
                            robotMap[oldPos[i].x + 1, oldPos[i].y].associate = true;
                            newPos[lengthListNewPos].x = (oldPos[i].x + 1);
                            newPos[lengthListNewPos].y = oldPos[i].y;
                            //devo tenere traccia del percorso che ho fatto!
                            journey[lengthJourney].box1.x = oldPos[i].x;
                            journey[lengthJourney].box1.y = oldPos[i].y;
                            journey[lengthJourney].box2.x = newPos[lengthListNewPos].x;
                            journey[lengthJourney].box2.y = newPos[lengthListNewPos].y;
                            lengthListNewPos++;
                            lengthJourney++;
                            if (((oldPos[i].x + 1) == goal.position.x) && (oldPos[i].y == goal.position.y))
                            {
                                isGoalReached = true;
                            }
                        }
                    }
                    //ovest
                    if (!robotMap[oldPos[i].x, oldPos[i].y].west)
                    {
                        if (!robotMap[oldPos[i].x - 1, oldPos[i].y].associate)
                        {
                            robotMap[oldPos[i].x - 1, oldPos[i].y].associate = true;
                            newPos[lengthListNewPos].x = (oldPos[i].x - 1);
                            newPos[lengthListNewPos].y = oldPos[i].y;
                            //devo tenere traccia del percorso che ho fatto!
                            journey[lengthJourney].box1.x = oldPos[i].x;
                            journey[lengthJourney].box1.y = oldPos[i].y;
                            journey[lengthJourney].box2.x = newPos[lengthListNewPos].x;
                            journey[lengthJourney].box2.y = newPos[lengthListNewPos].y;
                            lengthListNewPos++;
                            lengthJourney++;
                            if (((oldPos[i].x - 1) == goal.position.x) && (oldPos[i].y == goal.position.y))
                            {
                                isGoalReached = true;
                            }
                        }
                    }
                    i++;
                }
                //------------------------------------------------------------------------------------
                //copia la lista NewPos in OldPos e cancellala(NewPos)
                //CONTROLLARE SE FUNZIONA
                //NON SONO SICURO RIGUARDO LA POSIZIONE 0 E L'ULTIMA POSIZIONE
                for (int p = 0; p < lengthListNewPos; p++)
                {
                    oldPos[p].x = newPos[p].x;
                    oldPos[p].y = newPos[p].y;
                }
                lengthOldPos = lengthListNewPos;
                lengthListNewPos = 0;
            }
            //estrapolo il percorso
            //DA CONTROLLARE!!!
            bool wayFound = false;
            lengthListNewPos = 0;
            CellPos lastPos;
            lastPos.x = goal.position.x;
            lastPos.y = goal.position.y;
            newPos[lengthListNewPos].x = lastPos.x;
            newPos[lengthListNewPos].y = lastPos.y;
            lengthListNewPos++;
            while (!wayFound)
            {
                for (int a = 0; a < (lengthJourney); a++)
                { //fa un ciclo in piu'?
                    if ((journey[a].box2.x == lastPos.x) && (journey[a].box2.y == lastPos.y))
                    {
                        newPos[lengthListNewPos].x = journey[a].box1.x;
                        newPos[lengthListNewPos].y = journey[a].box1.y;
                        lengthListNewPos++;
                        lastPos.x = journey[a].box1.x;
                        lastPos.y = journey[a].box1.y;
                        if ((journey[a].box1.x == posx) && (journey[a].box1.y == posy))
                        {
                            wayFound = true;
                        }
                    }
                }
            }
            lengthJourneyBack = 0;
            //raddrizzo il percorso
            for (int b = ((lengthListNewPos) - 1); b > -1; b--)
            {
                journeyBack[lengthJourneyBack].x = newPos[b].x;
                journeyBack[lengthJourneyBack].y = newPos[b].y;
                lengthJourneyBack++;
            }
            //eseguo il percorso
            goToPos = true;
            //------------------------------------------------------------------------------------
        }
    }
    void GoToPos()
    {
        if ((listPos[(n_listPos - 1)].position.x != posx) || (listPos[(n_listPos - 1)].position.y != posy))
        {
            //devo arrivare alla casella goal
            //inizializzo le variabili e le liste
            bool isGoalReached = false;
            int lengthListNewPos = 0;
            int lengthOldPos = 0;
            int lengthJourney = 0;
            int i = 0;
            ResetMapAssociations();
            oldPos[lengthOldPos].x = posx;
            oldPos[lengthOldPos].y = posy;
            robotMap[oldPos[i].x, oldPos[i].y].associate = true;
            lengthOldPos++;

            //trovo il percorso
            while (!isGoalReached)
            {
                i = 0;
                while ((i < lengthOldPos) && (!isGoalReached))
                {
                    //nord
                    if (!robotMap[oldPos[i].x, oldPos[i].y].north)
                    {
                        if (!robotMap[oldPos[i].x, oldPos[i].y + 1].associate)
                        {
                            robotMap[oldPos[i].x, oldPos[i].y + 1].associate = true;
                            newPos[lengthListNewPos].x = oldPos[i].x;
                            newPos[lengthListNewPos].y = (oldPos[i].y + 1);
                            //devo tenere traccia del percorso che ho fatto!
                            journey[lengthJourney].box1.x = oldPos[i].x;
                            journey[lengthJourney].box1.y = oldPos[i].y;
                            journey[lengthJourney].box2.x = newPos[lengthListNewPos].x;
                            journey[lengthJourney].box2.y = newPos[lengthListNewPos].y;
                            lengthListNewPos++;
                            lengthJourney++;
                            for (int k = 0; k < (n_listPos); k++)
                            {
                                if ((oldPos[i].x == listPos[k].position.x) && ((oldPos[i].y + 1) == listPos[k].position.y))
                                {
                                    isGoalReached = true;
                                    posToReach.x = listPos[k].position.x;
                                    posToReach.y = listPos[k].position.y;
                                    element = k;
                                }
                            }
                        }
                    }
                    //sud
                    if (!robotMap[oldPos[i].x, oldPos[i].y].south)
                    {
                        if (!robotMap[oldPos[i].x, oldPos[i].y - 1].associate)
                        {
                            robotMap[oldPos[i].x, oldPos[i].y - 1].associate = true;
                            newPos[lengthListNewPos].x = oldPos[i].x;
                            newPos[lengthListNewPos].y = (oldPos[i].y - 1);
                            //devo tenere traccia del percorso che ho fatto!
                            journey[lengthJourney].box1.x = oldPos[i].x;
                            journey[lengthJourney].box1.y = oldPos[i].y;
                            journey[lengthJourney].box2.x = newPos[lengthListNewPos].x;
                            journey[lengthJourney].box2.y = newPos[lengthListNewPos].y;
                            lengthListNewPos++;
                            lengthJourney++;
                            for (int k = 0; k < (n_listPos); k++)
                            {
                                if ((oldPos[i].x == listPos[k].position.x) && ((oldPos[i].y - 1) == listPos[k].position.y))
                                {
                                    isGoalReached = true;
                                    posToReach.x = listPos[k].position.x;
                                    posToReach.y = listPos[k].position.y;
                                    element = k;
                                }
                            }
                        }
                    }
                    //est
                    if (!robotMap[oldPos[i].x, oldPos[i].y].east)
                    {
                        if (!robotMap[oldPos[i].x + 1, oldPos[i].y].associate)
                        {
                            robotMap[oldPos[i].x + 1, oldPos[i].y].associate = true;
                            newPos[lengthListNewPos].x = (oldPos[i].x + 1);
                            newPos[lengthListNewPos].y = oldPos[i].y;
                            //devo tenere traccia del percorso che ho fatto!
                            journey[lengthJourney].box1.x = oldPos[i].x;
                            journey[lengthJourney].box1.y = oldPos[i].y;
                            journey[lengthJourney].box2.x = newPos[lengthListNewPos].x;
                            journey[lengthJourney].box2.y = newPos[lengthListNewPos].y;
                            lengthListNewPos++;
                            lengthJourney++;
                            for (int k = 0; k < (n_listPos); k++)
                            {
                                if (((oldPos[i].x + 1) == listPos[k].position.x) && (oldPos[i].y == listPos[k].position.y))
                                {
                                    isGoalReached = true;
                                    posToReach.x = listPos[k].position.x;
                                    posToReach.y = listPos[k].position.y;
                                    element = k;
                                }
                            }
                        }
                    }
                    //ovest
                    if (!robotMap[oldPos[i].x, oldPos[i].y].west)
                    {
                        if (!robotMap[oldPos[i].x - 1, oldPos[i].y].associate)
                        {
                            robotMap[oldPos[i].x - 1, oldPos[i].y].associate = true;
                            newPos[lengthListNewPos].x = (oldPos[i].x - 1);
                            newPos[lengthListNewPos].y = oldPos[i].y;
                            //devo tenere traccia del percorso che ho fatto!
                            journey[lengthJourney].box1.x = oldPos[i].x;
                            journey[lengthJourney].box1.y = oldPos[i].y;
                            journey[lengthJourney].box2.x = newPos[lengthListNewPos].x;
                            journey[lengthJourney].box2.y = newPos[lengthListNewPos].y;
                            lengthListNewPos++;
                            lengthJourney++;
                            for (int k = 0; k < (n_listPos); k++)
                            {
                                if (((oldPos[i].x - 1) == listPos[k].position.x) && (oldPos[i].y == listPos[k].position.y))
                                {
                                    isGoalReached = true;
                                    posToReach.x = listPos[k].position.x;
                                    posToReach.y = listPos[k].position.y;
                                    element = k;
                                }
                            }
                        }
                    }
                    i++;
                }
                //------------------------------------------------------------------------------------
                //copia la lista NewPos in OldPos e cancellala(NewPos)
                //CONTROLLARE SE FUNZIONA
                //NON SONO SICURO RIGUARDO LA POSIZIONE 0 E L'ULTIMA POSIZIONE
                for (int p = 0; p < lengthListNewPos; p++)
                {
                    oldPos[p].x = newPos[p].x;
                    oldPos[p].y = newPos[p].y;
                }
                lengthOldPos = lengthListNewPos;
                lengthListNewPos = 0;
                //Debug.Log("Closest Cell: " + posToReach.x + " " + posToReach.y);
            }
            //estrapolo il percorso
            //DA CONTROLLARE!!!
            bool wayFound = false;
            lengthListNewPos = 0;
            CellPos lastPos;
            lastPos.x = posToReach.x;
            lastPos.y = posToReach.y;
            newPos[lengthListNewPos].x = lastPos.x;
            newPos[lengthListNewPos].y = lastPos.y;
            lengthListNewPos++;
            while (!wayFound)
            {
                for (int a = 0; a < (lengthJourney); a++)
                { //fa un ciclo in piu'?
                    if ((journey[a].box2.x == lastPos.x) && (journey[a].box2.y == lastPos.y))
                    {
                        newPos[lengthListNewPos].x = journey[a].box1.x;
                        newPos[lengthListNewPos].y = journey[a].box1.y;
                        lengthListNewPos++;
                        lastPos.x = journey[a].box1.x;
                        lastPos.y = journey[a].box1.y;
                        if ((journey[a].box1.x == posx) && (journey[a].box1.y == posy))
                        {
                            wayFound = true;
                        }
                    }
                }
            }
            lengthJourneyBack = 0;
            //raddrizzo il percorso
            for (int b = ((lengthListNewPos) - 1); b > -1; b--)
            {
                journeyBack[lengthJourneyBack].x = newPos[b].x;
                journeyBack[lengthJourneyBack].y = newPos[b].y;
                lengthJourneyBack++;
            }
            //eseguo il percorso
            goToPos = true;
            //------------------------------------------------------------------------------------
        }
    }
    void DeleteElement(int el)
    {
        //copio tutto tranne l'elemento da cancellare alla posizione el
        Positions[] copy = new Positions[n_listPos];
        int n = 0;
        for (int g = 0; g < n_listPos; g++)
        {
            if (g != el)
            {
                copy[n] = listPos[g];
                n++;
            }
        }
        for (int r = 0; r < n; r++)
        {
            listPos[r] = copy[r];
        }
    }
    void UpdateList()
    {
        //cancello le posizioni visitate dalla lista
        for (int g = 0; g < n_listPos; g++)
        {
            if (AlreadyVisited(listPos[g]))
            {
                DeleteElement(g);
            }
        }
    }
    void Start()
    {
        updateTime = PlayerPrefs.GetFloat("speed_value");
        robotMap = new Cell[(GetComponent<Maze>().lengthx + 1), (GetComponent<Maze>().lengthy + 1)];
        listPos = new Positions[GetComponent<Maze>().lengthx * GetComponent<Maze>().lengthy];
        newPos = new CellPos[GetComponent<Maze>().lengthx * GetComponent<Maze>().lengthy];
        oldPos = new CellPos[GetComponent<Maze>().lengthx * GetComponent<Maze>().lengthy];
        journeyBack = new CellPos[GetComponent<Maze>().lengthx * GetComponent<Maze>().lengthy];
        journey = new Way[GetComponent<Maze>().lengthx * GetComponent<Maze>().lengthy];
        //inizializzazione degli array
        for (int i = 0; i < (GetComponent<Maze>().lengthx + 1); i++)
        {
            for (int u = 0; u < (GetComponent<Maze>().lengthy + 1); u++)
            {
                robotMap[i, u].visited = false;
                robotMap[i, u].north = true;
                robotMap[i, u].south = true;
                robotMap[i, u].east = true;
                robotMap[i, u].west = true;
                robotMap[i, u].x = i;
                robotMap[i, u].y = u;
            }
        }
        //assegno la posizione iniziale a random
        posx = Random.Range(0, GetComponent<Maze>().lengthx);
        posy = Random.Range(0, GetComponent<Maze>().lengthy);
        SpawnPoint.position.x = posx;
        SpawnPoint.position.y = posy;
        SpawnPoint.orientation = "north";
        direction = "north";
        //da migliorare dando una rotazione a caso
        //aggiorno la posizione corrente (visitata) a true
        Debug.Log("Deployed on x= " + posx + " y= " + posy);
        robotMap[posx, posy].visited = true;
        //inserisco la posizione nella lista delle posizioni
        CheckPos("behind");
        CheckPos("left");
        CheckPos("right");
        CheckPos("forward");
    }
    void Update()
    {
        if (!isRobotStarted)
        {
            if (GetComponent<Maze>().deployRobot)
            {
                StartCoroutine(DeployRobot());
                isRobotStarted = true;
            }
        }
        else
        {
            if (GetComponent<Maze>().isMapCreated)
            {
                Robot.transform.position = new Vector3(posx, posy, -1);
            }
        }
        if (!isSpawnSpowned)
        {
            if (GetComponent<Maze>().isMapCreated)
            {
                StartingPoint.transform.position = new Vector3(posx, posy, -1);
                isSpawnSpowned = true;
            }
        }
    }
    IEnumerator DeployRobot()
    {
        bool home = false;
        Robot.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        while (true)
        {
            yield return new WaitForSeconds(updateTime);
            if (n_listPos > 0)
            {
                //controllo le posizioni della lista che non siano già state visitate
                UpdateList();
                if (!AlreadyVisited(listPos[(n_listPos - 1)]))
                {
                    //vai alla casella
                    GoToPos();
                    //Wavefront(listPos[(n_listPos - 1)]);
                    if (goToPos)
                    {
                        int valx;
                        int valy;
                        //Debug.Log(lengthJourneyBack);
                        for (int i = 0; i < (lengthJourneyBack - 1); i++)
                        {
                            //Debug.Log(i);
                            valx = journeyBack[i].x - journeyBack[(i + 1)].x;
                            valy = journeyBack[i].y - journeyBack[(i + 1)].y;

                            //Debug.Log(journeyBack[(i + 1)].x + " " + journeyBack[(i + 1)].y + " ");
                            if (valy == 1)
                            {
                                string sud = "south";
                                ComputeTurn(sud);
                                yield return new WaitForSeconds(updateTime);
                                Advance();
                                yield return new WaitForSeconds(updateTime);
                            }
                            else if (valy == -1)
                            {
                                string nord = "north";
                                ComputeTurn(nord);
                                yield return new WaitForSeconds(updateTime);
                                Advance();
                                yield return new WaitForSeconds(updateTime);
                            }
                            else if (valx == 1)
                            {
                                string ovest = "west";
                                ComputeTurn(ovest);
                                yield return new WaitForSeconds(updateTime);
                                Advance();
                                yield return new WaitForSeconds(updateTime);
                            }
                            else if (valx == -1)
                            { // valx==-1
                                string est = "east";
                                ComputeTurn(est);
                                yield return new WaitForSeconds(updateTime);
                                Advance();
                                yield return new WaitForSeconds(updateTime);
                            }
                        }
                        goToPos = false;

                        //infine mi giro nel verso dato da goal.orientation
                        ComputeTurn(listPos[(element)].orientation);

                        //Debug.Log("tolgo l'elemento");
                        //devo cancellare l'elemento dalla lista ( listPos[(element)] , non è detto che sia l'ultimo)
                        DeleteElement(element);//lo toglie dalla lista
                    }
                    else
                    {
                        //infine mi giro nel verso dato da goal.orientation
                        ComputeTurn(listPos[(n_listPos - 1)].orientation);
                    }
                    yield return new WaitForSeconds(updateTime);
                    n_listPos--;
                    GoForward();
                }
                else
                {
                    n_listPos--;
                }
            }
            else
            {
                if (!home)
                {
                    Wavefront(SpawnPoint);

                    if (goToPos)
                    {
                        int valx;
                        int valy;
                        //Debug.Log(lengthJourneyBack);
                        for (int i = 0; i < (lengthJourneyBack - 1); i++)
                        {
                            //Debug.Log(i);
                            valx = journeyBack[i].x - journeyBack[(i + 1)].x;
                            valy = journeyBack[i].y - journeyBack[(i + 1)].y;
                            if (valy == 1)
                            {
                                string sud = "south";
                                ComputeTurn(sud);
                                yield return new WaitForSeconds(updateTime);
                                Advance();
                                yield return new WaitForSeconds(updateTime);
                            }
                            else if (valy == -1)
                            {
                                string nord = "north";
                                ComputeTurn(nord);
                                yield return new WaitForSeconds(updateTime);
                                Advance();
                                yield return new WaitForSeconds(updateTime);
                            }
                            else if (valx == 1)
                            {
                                string ovest = "west";
                                ComputeTurn(ovest);
                                yield return new WaitForSeconds(updateTime);
                                Advance();
                                yield return new WaitForSeconds(updateTime);
                            }
                            else if (valx == -1)
                            { // valx==-1
                                string est = "east";
                                ComputeTurn(est);
                                yield return new WaitForSeconds(updateTime);
                                Advance();
                                yield return new WaitForSeconds(updateTime);
                            }
                        }
                        goToPos = false;
                    }
                    //infine mi giro nel verso dato da goal.orientation
                    ComputeTurn(SpawnPoint.orientation);
                    Debug.Log("RobotA: " + steps + " steps and " + turns + " turns = " + (steps + turns));
                    PlayerPrefs.SetString("ROBOTA", "RobotA: " + steps + " steps and " + turns + " turns = " + (steps + turns));
                    PlayerPrefs.SetInt("ROBOT_A", 1);
                    home = true;
                }
                yield return new WaitForSeconds(10);
            }
        }
    }
}