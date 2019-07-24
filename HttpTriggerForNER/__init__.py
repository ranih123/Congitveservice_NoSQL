import logging

import azure.functions as func
import spacy
import random
import os


def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')

    sentence = req.params.get('sentence')
    print(sentence)
    
    if sentence:
        try:
            path = os.getcwd() + r"\HttpTriggerForNER\ner-model"
            print(path)
            
            nlp2 = spacy.load(path)
        except Exception as ex:
            print(" An exception occured")
        
        doc = nlp2(sentence)
        print("Entities", [(ent.text, ent.label_) for ent in doc.ents])
        
        jsonObj = {
            "TableNames": [],
            "Columns": []
        }
        
        colObj = {}
        for i, ent in doc.ents:
            if ent.label_ == 'Table':
                jsonObj["TableNames"].append(ent.text)
            
            if ent.label_ == 'Column':
                if i == 1:
                    colObj = {
                        "ColumnName": "",
                        "ColumnValue": "",
                        "ColumnOperator": ""
                    }
                else:
                    jsonObj["Columns"].append(colObj)
                    colObj = {
                        "ColumnName": "",
                        "ColumnValue": "",
                        "ColumnOperator": ""
                    }
                colObj["columnName"].append(ent.text)
            if ent.label_ == 'Value':
                colObj["columnValue"].append(ent.text)
            if ent.label_ == 'Operator':
                colObj["columnOperator"].append(ent.text)
        
        return func.HttpResponse(jsonObj)

    else:
        return func.HttpResponse(
             "Please pass a sentence on the query string or in the request body",
             status_code=400
        )
