"""
lucidAPI is a library containing A&D classes to work with a lucidchart CSV
Last Edited By: Dan
Last Edited: 12/19/22
Edit Note: Finished first iteration of the Lucid_player class which turns a lucidchart into a text adventure
Future update will include a way to keep track of visited nodes to prevent reprint of old texts

Example Usage for Aaron:
test = lucidAPI.Lucid_player("./scene1.csv")
test.play()
"""

import pandas as pd
from collections import defaultdict
import os

"""
CSVTree transforms CSV data from an exported lucidchart into a graph which is accessed using the following three variables
text_map: (Object type: dictionary) (Key: Id of lucidchart node, int) (Element: text of lucidchart node, string)
children_map: (Object type: dictionary) (Key: Id of lucidchart node, int) (Element: List of connected lucidchart nodes by their Id, List(int))
root: (Object type: int) The Id of the terminator node in a lucidchart. Terminator node defines the starting node.
The terminator node is a specific type of box found in the lucidchart flowchart as the third option. Shaped like an oval and is more
rounded then the typical nodes
"""
class CSVTree:
    def __init__(self, csvdata):
        n,m = csvdata.shape
        self.text_map = {}
        self.children_map = defaultdict(list)
        self.root = csvdata.index[csvdata["Name"] == "Terminator"][0]

        index = 1
        while csvdata.loc[index]["Name"] != "Line":
            self.text_map[index] = csvdata.loc[index]["Text"].replace("\u2028", " ")
            index += 1
        while index <= n:
            self.children_map[csvdata.loc[index]["Line Source"]].append(csvdata.loc[index]["Line Destination"])
            index += 1


"""
Lucid_player provides a class that will take a lucidchart CSV and turn it into a playable text adventure
Constructor Parameters: string holding the path to the CSV file
Note that it only works with proper inputs as this is a dev tool and not a shippable product

Example Usage:
test = lucidAPI.Lucid_player("./scene1.csv")
test.play()
"""
class Lucid_player:
    def load(self, path):
        data = pd.read_csv(path,index_col=0,header=0)
        data = data.rename(columns={"Text Area 1": "Text"})
        print("loaded " + data.loc[1]["Text"])
        return CSVTree(data)

    def __init__(self, path):
        self.tree = self.load(path)

    def play(self):
        print("Begining " + self.tree.text_map[1] + "\n")
        curIndex = self.tree.root
        while True:
            print(self.tree.text_map[curIndex])
            #print(len(self.tree.children_map[curIndex]))
            if (len(self.tree.children_map[curIndex]) == 0):
                print("End of Conversation")
                break
            elif (len(self.tree.children_map[curIndex]) == 1):
                input("\nInput anything to continue\n")
                os.system('cls')
                curIndex = self.tree.children_map[curIndex][0]
            else:
                print()
                for i in range(len(self.tree.children_map[curIndex])):
                    print(str(i) + ": " + self.tree.text_map[self.tree.children_map[curIndex][i]] + "\n")
                print()
                temp = int(input())
                curIndex = self.tree.children_map[curIndex][temp]
                os.system('cls')
                #print("\n" + self.tree.text_map[curIndex] + "\n")

