import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { AppPagePaths, AppApiPaths } from '../Api-authorization/AppConstants';

export class OrderedProductCard extends Component {

    constructor(props) {
        super(props);

        this.handleCancel = this.handleCancel.bind(this);
    }

    async handleCancel() {
        let response = await fetch(AppApiPaths.CartedProductCancel + '/' + (this.props.product.id), {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        });

        if (!response.ok)
            console.log("error");
        else {
            this.props.updateCart();
        }

    }

    render() {
        const product = this.props.product;
        const countOfDeferredProduct = this.props.countOfDeferredProduct;

        return (
            <div className="card mt-3 bg-light">
                <Link className="row no-gutters align-items-center carted_card" to={AppPagePaths.Product + "/" + product.id}>
                    <div className="col-md-4 ">
                        <img src={product.linkToImage} width="100%" />
                    </div>
                    <div className="col-md-8">
                        <div className="card-body">
                            <h5 className="card-title">{product.name}</h5>
                            <p className="card-text"><small className="text-muted">Опубликовано: {product.dateOfPublication}</small></p>
                            <div className="d-flex justify-content-between ">
                                <div className="">
                                    <h5 className="">Цена: {product.price}$</h5>
                                </div>
                                <div className="">
                                    <h5 className="">Количество: {countOfDeferredProduct}</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </Link>
                <div className="row no-gutters mt-3">
                    <div className="col-md text-center"><h5><b>Итого: {countOfDeferredProduct * product.price}$</b></h5></div>
                    <div className="col-md"><button type="button" className="btn btn-secondary btn-block" onClick={() => this.handleCancel()}>Отказаться</button></div>
                </div>
            </div>);
    }
}