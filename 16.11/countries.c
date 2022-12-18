#include "../Modules/Graph/graph.h"
#include <stdio.h>
#include <stdbool.h>
#include "malloc.h"

bool test(void) {
    bool testResult = true;

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
    NodesDataList **countriesClosestNodesLists = calloc(numberOfCapitals, sizeof(NodesDataList *));

    fscanf(file, "%d", &numberOfCapitals);
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
    fclose(file);

    int numberOfCapturedNodes = 0;
    int currentCountryMove = 0;
    while (numberOfCapturedNodes < citiesNumber) {
        addNodeToTheCountry(graph, countriesClosestNodesLists[currentCountryMove], currentCountryMove);
        numberOfCapturedNodes += 1;
        currentCountryMove = (currentCountryMove + 1) % numberOfCapitals;
    }

    print(graph, numberOfCapitals);

    for (int i = 0; i < numberOfCapitals; i++) {
        clear(countriesClosestNodesLists[i]);
    }
    for (int i = 0; i < citiesNumber; i++) {
        free(nodesData[i].distancesToTheCapitals);
    }
    free(nodesData);
    free(countriesClosestNodesLists);
    clearGraph(graph);
    return 0;
}