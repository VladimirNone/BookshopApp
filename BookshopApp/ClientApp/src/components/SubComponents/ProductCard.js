import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { AppPagePaths } from '../Api-authorization/AppConstants';

export class ProductCard extends Component {

    render() {
        const product = this.props.product;

        return (
            <Link className="card mt-3 bg-light product_card" to={AppPagePaths.Product + "/" + product.id}>
                <div className="row no-gutters align-items-center">
                    <div className="col-md-4">
                        <img src={product.linkToImage} width="100%" />
                    </div>
                    <div className="col-md-8">
                        <div className="card-body">
                            <h5 className="card-title">{product.name}</h5>
                            {/*Need use .text-truncate */}
                            <p className="card-text">{product.description}</p>
                            <p className="card-text text-right"><b>{product.price}$</b></p>
                            <p className="card-text"><small className="text-muted">Опубликовано: {(new Date(product.dateOfPublication)).toLocaleString()}</small></p>
                        </div>
                    </div>
                </div>
            </Link>);
    }
}