import React, { Component, Fragment } from 'react';
import { ApplicationApiPaths } from '../Api-authorization/AppConstants';

export class Product extends Component {

    constructor(props) {
        super(props);

        this.state = {
            productId: parseInt(props.match.params.id),
            product: null
        }
    }

    componentDidMount() {
        this.getProductFromServer();
    }

    async getProductFromServer() {
        let response = await fetch(ApplicationApiPaths.Product + '/' + (this.state.productId), {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        });

        if (!response.ok) {
            console.log(await response.json())
        }
        else {
            const answer = await response.json();
            this.setState({ product: answer });
        }
    }



    render() {
        const product = this.state.product;

        if (product == null)
            return (<div />);

        return (
            <Fragment>
                <div className="row" >
                    <img src={product.linkToImage} width="100%" />
                </div>
                <div className="row mx-2 mt-3">
                    <div className="product-details-content-area">
                        <div className="product-details-text">
                            <h4 className="">{product.name}</h4>
                            <h5 className="">Автор: {product.author.firstName + " " + product.author.lastName}</h5>
                            <div className="d-flex mb-3 mr-3">
                                <h5 className="flex-grow-1">В наличии: {product.countInStock}</h5>
                                <h5 className=""><b>{product.price}$</b></h5>
                            </div>
                            <p className="product-description text-justify">{product.description}</p>
                        </div>
                        <div className="product-buy d-flex justify-content-end input-group ">
                            <form className="form-inline mr-3 ">
                                <div className="form-group mb-2">
                                    <p className="form-control-plaintext"><span>Количество</span></p>
                                </div>
                                <div className="form-group mx-3 mb-2">
                                    <input className="form-control" min="1" max="100" defaultValue="1" type="number" />
                                </div>
                                <button className="btn btn-primary mb-2">Добавить в корзину</button>
                            </form>
                        </div>
                    </div>
                </div>
            </Fragment>);
    }
}
