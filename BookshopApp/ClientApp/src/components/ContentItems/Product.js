import React, { Component } from 'react';
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
        if (this.state.product == null)
            return (<div />);

        return (
            <div>Product Name: {this.state.product.name}</div>);
    }
}
