import json;
import re;
import tempfile;
import os;
import PyPDF2;
import random;
from flask import Flask, request, jsonify;
import pandas as pd;
#from docquery import document; #, pipeline;
#import spacy;
from flair.data import Sentence;
from flair.models import SequenceTagger;
 

#nlp = spacy.load('en_core_web_sm') adza
 

tagger = SequenceTagger.load('ner')
## Standardized question
#standard_question = "What is the client?"

# Function to process the PDF file and return the answer
def processPDF(cvFile):
    # Save the uploaded PDF file to a temporary directory
    tempDirPath = tempfile.mkdtemp()
    pdfPath = os.path.join(tempDirPath,f"cv_{random.randint(1000000000,999999999)}.pdf")
    cvFile.save(pdfPath)
    txt = extract_text_from_pdf(pdfPath)
    #email = extract_emails(txt)
    #nlp1 = spacy.load('en_core_web_sm')

    #sentence = Sentence(txt)
    ## Run NER on the sentence
    #tagger.predict(sentence)
    #os.environ["KMP_DUPLICATE_LIB_OK"] = "TRUE"
    #arabic_names = ""
    #for entity in sentence.get_spans('ner'):
    #    if entity.tag == 'PER' and any(char.isalpha() for char in entity.text):
    #        arabic_names = arabic_names + entity.text + ","
    ## Load the document and create the pipeline
    #doc = document.load_document(pdfPath)
    ##p = pipeline('document-question-answering')
#
    #nlp_text = nlp1(txt)
    #noun_chunks = nlp_text.noun_chunks
    ## removing stop words and implementing word tokenization
    #tokens = [token.text for token in nlp_text if not token.is_stop]
    #
    ## reading the csv file
    #data = pd.read_csv(r'C:\Users\fateh\PycharmProjects\Skill_Extraction\res\skills (1).csv')
    #
    ## extract values
    #skills = data.columns.values.tolist()
    #skillset = ""
    #
    ## check for one-grams (example: python)
    #for token in tokens:
    #    if token.lower() in skills:
    #
    #        skillset = skillset+token+","
    #
    ## check for bi-grams and tri-grams (example: machine learning)
    #for token in noun_chunks:
    #    token = token.text.lower().strip()
    #    if token in skills:
    #        skillset = skillset+token+","




    # Process the standardized question using the pipeline and document context
    #result = p(question=standard_question, **doc.context)
    #client = result[0]['answer']

    # Delete the temporary directory and its contents
    #os.remove(pdfPath)
    #os.rmdir(tempDirPath)


    return {"name":"profile name"}#[client,skillset,email,arabic_names]

# Function to extract emails from text
def extract_emails(text):
    # Regular expression for extracting emails
    email_regex = r'\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b'
    for email in re.findall(email_regex, text):
        return email

def extract_text_from_pdf(pdfPath):
    text = ""
    with open(pdfPath, "rb") as file:
        pdf_reader = PyPDF2.PdfReader(file)
        num_pages = len(pdf_reader.pages)
        for page_num in range(num_pages):
            page = pdf_reader.pages[page_num]
            text += page.extract_text()
    return text


app = Flask(__name__)

if __name__ == "__main__":
    app.run(debug=True)

@app.route("/analyse",methods=["POST"])
def analyseCV():
    print("calling analyseCV")
    try:
        filePartKey="file"
        # Check if a file was uploaded
        if filePartKey not in request.files:
            return jsonify({"error":"No file part"})

        cvFile = request.files[filePartKey]
        #proj = request.form.get('Project')
        #refId = request.form.get('Id')
        #print(proj)
        #print(refId)
        # Check if the file is of PDF type

        #checkFile(cvFile)

        # Process the PDF file
        #answer = processPDF(cvFile)

        #json = json.dumps(answer)
        #data = json.loads(jsret)
        #print(data[2],data[1],data[0],data[3],proj,refId)

        #save_to_database(data[2],data[0],data[3],data[1],proj,refId)
        # Return the answer as JSON response
        return {"fileName":cvFile.filename}
    except Exception as exception:
        return jsonify({"error":exception.__str__()})

def checkFile(file):
    fileName=file.filename
    if fileName=="" or not fileName.endswith(".pdf"):
        raise Exception("Invalid file format. Please upload a PDF file.")
