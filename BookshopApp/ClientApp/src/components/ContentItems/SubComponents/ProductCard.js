import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { ApplicationPagePaths } from '../../Api-authorization/AppConstants';

export class ProductCard extends Component {

    render() {
        const product = this.props.product;

        return (
            <Link className="card mt-3" to={ApplicationPagePaths.Product + "/" + product.id}>
                <div className="row no-gutters">
                    <div className="col-md-4">
                        <img src={product.linkToImage} width="100%" />
                    </div>
                    <div className="col-md-8">
                        <div className="card-body">
                            <h5 className="card-title">{product.name}</h5>
                            {/*Need use text-truncate */}
                            <p className="card-text">{product.description}</p>
                            <p className="card-text text-right"><b>{product.price}$</b></p>
                            <p className="card-text"><small className="text-muted">{product.dateOfPublication}</small></p>
                        </div>
                    </div>
                </div>
            </Link>);
    }
}