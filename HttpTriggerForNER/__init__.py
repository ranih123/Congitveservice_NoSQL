import logging

import azure.functions as func
import spacy
import random


def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')

    sentence = req.params.get('sentence')
    if not name:
        try:
            req_body = req.get_json()
        except ValueError:
            pass
        else:
            sentence = req_body.get('sentence')

    if sentence:
        sentence = "Get employee who lives in state Washington"
        nlp2 = spacy.load(output_dir)
        doc = nlp2(sentence)
        print("Entities", [(ent.text, ent.label_) for ent in doc.ents])
        
        return func.HttpResponse(f"Hello {name}!")

    else:
        return func.HttpResponse(
             "Please pass a sentence on the query string or in the request body",
             status_code=400
        )
