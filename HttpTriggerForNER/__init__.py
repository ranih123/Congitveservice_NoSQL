import logging
import azure.functions as func
import spacy
import random
import os
import json


def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')

    sentence = req.params.get('sentence')
    print(sentence)
    
    if sentence:
        jsonObj = {
            "TableNames": list(),
            "Columns": list()
        }

        try:
            # create empty struture.
            nlp = spacy.blank('en')
            ner_pipe = nlp.create_pipe('ner')
            nlp.add_pipe(ner_pipe)

            path = os.getcwd() + r"\HttpTriggerForNER\nlp-model"
            print(path)
            
            # read the model from binary data.
            byte_file_nlp = open(path, 'rb').read()
            nlp.from_bytes(byte_file_nlp)
        
        
            # parse the sentence using nlp model.
            doc = nlp(sentence)
            print("Entities", [(ent.text, ent.label_) for ent in doc.ents])
            
            colObj = {
                "ColumnName": "",
                "ColumnValue": "",
                "LogicalOperator": "",
                "Comparator": ""
            }
            colObj2 = {
                "ColumnName": "",
                "ColumnValue": "",
                "LogicalOperator": "",
                "Comparator": ""
            }
            for i, ent in enumerate(doc.ents):
                if ent.label_ == 'Table':
                    jsonObj["TableNames"].append(ent.text)
                
                # for format len(3) - table - col - value
                if len(doc.ents)  == 3:
                    if ent.label_ == 'Column':
                        colObj["ColumnName"] = ent.text
                    if ent.label_ == 'Value':
                        colObj["ColumnValue"] = ent.text
                        colObj["Comparator"] = 'equal to'
                
                # for format len(4) - table - col - compara - value
                if len(doc.ents)  == 4:
                    if ent.label_ == 'Column':
                        colObj["ColumnName"] = ent.text
                    if ent.label_ == 'Comparator':
                        colObj["Comparator"] = ent.text
                    if ent.label_ == 'Value':
                        colObj["ColumnValue"] = ent.text 
                
                # for format len(6) - table - col - value - logicOp - col - value
                if len(doc.ents)  == 6:
                    if i == 4:
                        if ent.label_ == 'Column':
                            colObj2["ColumnName"] = ent.text
                        if ent.label_ == 'Value':
                            colObj2["ColumnValue"] = ent.text
                            colObj["Comparator"] = 'equal to'
                    else:
                        if ent.label_ == 'Column':
                            colObj["ColumnName"] = ent.text
                        if ent.label_ == 'Value':
                            colObj["ColumnValue"] = ent.text
                            colObj["Comparator"] = 'equal to'
                        if ent.label_ == 'LogicalOperator':
                            colObj["LogicalOperator"] = ent.text

                
                # for format len(7) - table - col -  value - logicOp - col - compara - value
                if len(doc.ents)  == 7:
                    if i == 4:
                        if ent.label_ == 'Column':
                            colObj2["ColumnName"] = ent.text
                        if ent.label_ == 'Comparator':
                            colObj2["Comparator"] = ent.text
                        if ent.label_ == 'Value':
                            colObj2["ColumnValue"] = ent.text
                    else:
                        if ent.label_ == 'Column':
                            colObj["ColumnName"] = ent.text
                        if ent.label_ == 'Value':
                            colObj["ColumnValue"] = ent.text
                            colObj["Comparator"] = 'equal to'
                        if ent.label_ == 'LogicalOperator':
                            colObj["LogicalOperator"] = ent.text
                
                # for format len(8) - table - col - compara - value - logicOp - col - compara - value
                if len(doc.ents)  == 8:
                    if i == 5:
                        if ent.label_ == 'Column':
                            colObj2["ColumnName"] = ent.text
                        if ent.label_ == 'Comparator':
                            colObj2["Comparator"] = ent.text
                        if ent.label_ == 'Value':
                            colObj2["ColumnValue"] = ent.text
                    else:
                        if ent.label_ == 'Column':
                            colObj["ColumnName"] = ent.text
                        if ent.label_ == 'Comparator':
                            colObj["Comparator"] = ent.text
                        if ent.label_ == 'Value':
                            colObj["ColumnValue"] = ent.text
                        if ent.label_ == 'LogicalOperator':
                            colObj["LogicalOperator"] = ent.text


            # add colObj to to jsonObj
            jsonObj["Columns"].append(colObj)
            if len(doc.ents) == 6 or len(doc.ents) == 7 or len(doc.ents) == 8:
                jsonObj["Columns"].append(colObj2)

            print(jsonObj)
        except Exception as ex:
            print(" An exception occured")

        return func.HttpResponse(json.dumps(jsonObj))

    else:
        return func.HttpResponse(
             "Please pass a sentence on the query string or in the request body",
             status_code=400
        )
