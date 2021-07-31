import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { AppPagePaths } from '../../Api-authorization/AppConstants';

export class CartedCard extends Component {

    constructor(props) {
        super(props);

        this.handleCancel = this.handleCancel.bind(this);
    }

    async handleCancel() {

    }

    render() {
        const product = this.props.product;
        const countOfDeferredProduct = this.props.countOfDeferredProduct;

        return (
            <div className="card mt-3">
                <Link className="row no-gutters carted_card" to={AppPagePaths.Product + "/" + product.id}>
                    <div className="col-md-4">
                        <img src={product.linkToImage} width="100%" />
                    </div>
                    <div className="col-md-8">
                        <div className="card-body">
                            <h5 className="card-title">{product.name}</h5>
                            <p className="card-text text-right"><b>{product.price}$</b></p>
                            <p className="card-text"><small className="text-muted">Опубликовано: {product.dateOfPublication}</small></p>
                        </div>
                    </div>
                </Link>
                <div className="row no-gutters mt-2">
                    <div className="col-md">
                        <h4>
                            Количество: {countOfDeferredProduct}
                        </h4>
                    </div>
                    <div className="col-md">
                        <button type="button" className="btn btn-primary" onClick={() => this.handleCancel()}>Отказаться</button>
                    </div>
                </div>
            </div>);
    }
}