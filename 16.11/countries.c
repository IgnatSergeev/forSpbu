#include "../Modules/Graph/graph.h"
#include <stdio.h>
#include <stdbool.h>
#include "malloc.h"

void readGraphEdgesAndCapitalsAndAssignCitiesToTheCountries(FILE *file, Graph *graph, int roadsNumber, int citiesNumber) {
    for (int i = 0; i < roadsNumber; i++) {
        int startCityIndex = 0;
        int endCityIndex = 0;
        int length = 0;
        fscanf(file, "%d %d %d", &startCityIndex, &endCityIndex, &length);
        startCityIndex -= 1;
        endCityIndex -= 1;
        EdgeProperties edgeProperties = {endCityIndex, length};
        addEdge(graph, startCityIndex, edgeProperties);
        edgeProperties.endNodeIndex = startCityIndex;
        addEdge(graph, endCityIndex, edgeProperties);
    }

    int numberOfCapitals = 0;
    fscanf(file, "%d", &numberOfCapitals);
    NodesDataList **countriesClosestNodesLists = calloc(numberOfCapitals, sizeof(NodesDataList *));

    assignGraphsNumberOfCapitals(graph, numberOfCapitals);
    for (int i = 0; i < numberOfCapitals; i++) {
        int capitalIndex = 0;
        fscanf(file, "%d", &capitalIndex);
        capitalIndex -= 1;
        NodeData nodeData = getNodeData(graph, capitalIndex);
        nodeData.countryIndex = i;
        nodeData.distancesToTheCapitals[i] = 0;
        changeNodeData(graph, capitalIndex, nodeData);

        NodesDataList *list = create();
        insertNodeToEnd(list, nodeData);
        countriesClosestNodesLists[i] = list;
    }

    int numberOfCapturedNodes = 0;
    int currentCountryMove = 0;
    while (numberOfCapturedNodes < citiesNumber) {
        addNodeToTheCountry(graph, countriesClosestNodesLists[currentCountryMove], currentCountryMove);
        numberOfCapturedNodes += 1;
        currentCountryMove = (currentCountryMove + 1) % numberOfCapitals;
    }

    for (int i = 0; i < numberOfCapitals; i++) {
        clear(countriesClosestNodesLists[i]);
    }
    free(countriesClosestNodesLists);
}

bool test(void) {
    bool testResult = true;
    FILE *testFile = fopen("../16.11/countriesTest.txt", "r");
    if (testFile == NULL) {
        return false;
    }

    int citiesNumber = 0;
    int roadsNumber = 0;
    fscanf(testFile, "%d", &citiesNumber);
    fscanf(testFile, "%d", &roadsNumber);

    NodeData *nodesData = calloc(citiesNumber, sizeof(NodeData));
    for (int i = 0; i < citiesNumber; i++) {
        nodesData[i].countryIndex = -1;
        nodesData[i].index = i;
        nodesData[i].distancesToTheCapitals = calloc(citiesNumber, sizeof(int));
        for (int j = 0; j < citiesNumber; j++) {
            nodesData[i].distancesToTheCapitals[j] = -1;
        }
    }

    Graph *testGraph = createGraph(citiesNumber, nodesData);
    if (testGraph == NULL) {
        for (int i = 0; i < citiesNumber; i++) {
            free(nodesData[i].distancesToTheCapitals);
        }
        free(nodesData);
        fclose(testFile);
        return false;
    }

    readGraphEdgesAndCapitalsAndAssignCitiesToTheCountries(testFile, testGraph, roadsNumber, citiesNumber);
    fclose(testFile);

    int **countriesProperties = print(testGraph, true);
    for (int i = 0; i < 4; i++) {
        if (countriesProperties[0][i] != 1) {
            testResult = false;
        }
    }
    for (int i = 4; i < 7; i++) {
        if (countriesProperties[0][i] != 0) {
            testResult = false;
        }
    }

    for (int i = 0; i < 4; i++) {
        if (countriesProperties[1][i] != 0) {
            testResult = false;
        }
    }
    for (int i = 4; i < 7; i++) {
        if (countriesProperties[1][i] != 1) {
            testResult = false;
        }
    }

    for (int i = 0; i < citiesNumber; i++) {
        free(nodesData[i].distancesToTheCapitals);
    }
    for (int i = 0; i < 2; i++) {
        free(countriesProperties[i]);
    }
    free(countriesProperties);
    free(nodesData);
    clearGraph(testGraph);
    return testResult;
}

int main(void) {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    }
    printf("Тесты пройдены\n");

    printf("Программа прочитает данные из условия задачи из файла countries.txt\n");
    FILE *file = fopen("../16.11/countries.txt", "r");
    if (file == NULL) {
        printf("Файл не найден\n");
        return -1;
    }

    int citiesNumber = 0;
    int roadsNumber = 0;
    fscanf(file, "%d", &citiesNumber);
    fscanf(file, "%d", &roadsNumber);

    NodeData *nodesData = calloc(citiesNumber, sizeof(NodeData));
    for (int i = 0; i < citiesNumber; i++) {
        nodesData[i].countryIndex = -1;
        nodesData[i].index = i;
        nodesData[i].distancesToTheCapitals = calloc(citiesNumber, sizeof(int));
        for (int j = 0; j < citiesNumber; j++) {
            nodesData[i].distancesToTheCapitals[j] = -1;
        }
    }

    Graph *graph = createGraph(citiesNumber, nodesData);
    if (graph == NULL) {
        for (int i = 0; i < citiesNumber; i++) {
            free(nodesData[i].distancesToTheCapitals);
        }
        free(nodesData);
        fclose(file);
        return -1;
    }

    readGraphEdgesAndCapitalsAndAssignCitiesToTheCountries(file, graph, roadsNumber, citiesNumber);
    fclose(file);

    print(graph, false);

    for (int i = 0; i < citiesNumber; i++) {
        free(nodesData[i].distancesToTheCapitals);
    }
    free(nodesData);
    clearGraph(graph);
    return 0;
}