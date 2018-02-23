from flask import Flask, jsonify, request, make_response

app = Flask(__name__)

images = [
    {
        'id': 1,
        'imageName': 'dsajbflbfwobsbdcjsabiADIUEBFSKLJBFSHJBA'
    },
    {
        'id': 2,
        'imageName': 'sdbfuerhgfyubfdjbvuhbuoabfoabguoegbshai'
    }
]


@app.route('/')
def index():
    return "Hello World!"


@app.route('/circuit/api/v1.0/image', methods=['GET'])
def get_image():
    return jsonify({'images': images})


@app.route('/circuit/api/v1.0/image', methods=['POST'])
def post_image():
    # !if not request.json or not 'imageName' in request.json:
    # !abort(400)
    image = {
        'id': images[-1]['id'] + 1,
        'imageName': request.json['imageName']
    }

    images.append(image)
    return jsonify({'image': image}), 201


if __name__ == '__main__':
    app.run(debug=True)
