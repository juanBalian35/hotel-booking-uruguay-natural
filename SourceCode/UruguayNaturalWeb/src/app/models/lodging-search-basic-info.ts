import {LodgingImageBasicInfo} from './lodging-image-basic-info';

export interface LodgingSearchBasicInfo {
    name: string;
    rating: number;
    address: string;
    description: string;
    pricePerNight: number;
    totalPrice: number;
    reviewsAverage: number;
    reviewsQuantity: number;
    images: LodgingImageBasicInfo[];
    isFull: boolean;
    id: number;
}
