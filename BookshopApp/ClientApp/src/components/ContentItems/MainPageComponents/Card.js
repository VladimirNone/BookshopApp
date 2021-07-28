import React, { Component } from 'react';

export class Card extends Component {

    render() {
        const product = this.props.product;

        return (
            <div className="card mt-3">
                <div className="row no-gutters">
                    <div className="col-md-4">
                        <img src={product.linkToImage} width="100%" />
                    </div>
                    <div className="col-md-8">
                        <div className="card-body">
                            <h5 className="card-title">{product.name}</h5>
                            <p className="card-text">{product.description}</p>
                            <p className="card-text text-right"><b>{product.price}$</b></p>
                            <p className="card-text"><small className="text-muted">{product.dateOfPublication}</small></p>
                        </div>
                    </div>
                </div>
            </div>);
    }
}